using Application.Services;
using Domain.DTOs.IdentityDTOs;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IBorrowerService _borrowerService;
        private readonly ToolContext _context;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration, IBorrowerService borrowerService, ToolContext context)
        {
            _userManager = userManager;
            _configuration = configuration;

            _borrowerService = borrowerService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var user = new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto, CancellationToken ct)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);

            if (user == null)
            {
                return Unauthorized("user null");
            }
            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return Unauthorized("password error");
            }

            var borrower = await _borrowerService.GetByUserIdAsync(user.Id, ct);

            var refreshToken = GenerateRefreshToken();

            var entity = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                IsRevoked = false
            };

            await _borrowerService.AddRefreshToken(entity, ct);

            var jwttoken = await GenerateJwtTokenAsync(user);
            return Ok(new { token = jwttoken, refresh = refreshToken, user = borrower });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDTO dto, CancellationToken ct)
        {
            var refreshTokenEntity = await _context.RefreshTokens
        .FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken && !rt.IsRevoked);

            if (refreshTokenEntity == null || refreshTokenEntity.Expires < DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token.");

            var user = await _userManager.FindByIdAsync(refreshTokenEntity.UserId);
            if (user == null)
                return Unauthorized();

            // Revoke old token
            refreshTokenEntity.IsRevoked = true;

            // Generate new tokens
            var newJwtToken = await GenerateJwtTokenAsync(user);
            var newRefreshToken = GenerateRefreshToken();

            var newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };
            await _context.RefreshTokens.AddAsync(newRefreshTokenEntity);
            await _context.SaveChangesAsync();

            return Ok(new { token = newJwtToken, refreshToken = newRefreshToken });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshRequestDTO dto)
        {
            var entity = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken && !rt.IsRevoked);

            if (entity == null)
            {
                return BadRequest("Invalid refresh token.");
            }

            entity.IsRevoked = true;

            await _context.SaveChangesAsync();

            return Ok("Logged out successfully.");  
        }

        [Authorize(Roles="Admin, User")]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await RevokeAllUserRefreshTokens(user.Id);
            return Ok("Password changed successfully.");
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return NotFound("User not found.");
            }
            await _userManager.DeleteAsync(user);
            await RevokeAllUserRefreshTokens(user.Id);
            return Ok("User deleted");
        }



        private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
   
            var jwt = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user); // Fetch user roles if needed

            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roleClaims);

            var token = new JwtSecurityToken(
                issuer: "TooliRentAPI",
                audience: "TooliRentUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }

            
        }

        private async Task RevokeAllUserRefreshTokens(string userId)
        {
            var tokens = await _context.RefreshTokens
                .Where(rt => rt.UserId == userId && !rt.IsRevoked)
                .ToListAsync();
            foreach (var token in tokens)
            {
                token.IsRevoked = true;
            }
            await _context.SaveChangesAsync();
        }
    }
}
