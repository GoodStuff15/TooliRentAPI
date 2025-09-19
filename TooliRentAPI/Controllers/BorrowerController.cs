using Application.Services;
using Application.Validators.DataValidation;
using Domain.DTOs;
using Domain.DTOs.ResponseDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerService _borrowerService;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IValidator<BorrowerCreateDTO> _createValidator;
        private readonly IValidator<BorrowerUpdateDTO> _updateValidator;
        public BorrowerController(IBorrowerService borrowerService, IValidator<BorrowerUpdateDTO> updateValidator,
                                                                    IValidator<BorrowerCreateDTO> createValidator,
                                                                    UserManager<IdentityUser> userManager)
        {
            _borrowerService = borrowerService;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
            _userManager = userManager;
        }

        protected string GetUserId()
        {
            return this.User.Claims.First(i => i.Type == "UserId").Value.ToString();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowerReadDTO>>> GetAllBorrowers(CancellationToken ct)
        {
            var borrowers = await _borrowerService.GetAllAsync();
            return Ok(borrowers);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowerReadDTO>> GetBorrowerById(int id, CancellationToken ct)
        {

            var borrower = await _borrowerService.GetByIdAsync(id, ct);
            if (borrower == null)
            {
                return NotFound();
            }
            return Ok(borrower);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<BorrowerReadDTO>>> GetFilteredBorrowers([FromQuery] BorrowerFilterDTO filterDto, CancellationToken ct)
        {
            var borrowers = await _borrowerService.GetAllFilteredAsync(filterDto, ct);
            return Ok(borrowers);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<ActionResult<BorrowerCreate_ResponseDTO>> CreateBorrower([FromBody] BorrowerCreateDTO createDto, CancellationToken ct)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto, ct);

            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _borrowerService.CreateAsync(createDto, ct);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrower(int id, [FromBody] BorrowerUpdateDTO updateDto, CancellationToken ct)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto, ct);

            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            
            }

            var result = await _borrowerService.UpdateAsync(id, updateDto, ct);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrower(int id, CancellationToken ct)
        {
            var result = await _borrowerService.DeleteAsync(id, ct);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("/active/{id}")]
        public async Task<IActionResult> HandleActiveStatus(int id, CancellationToken ct)
        {
            var result = await _borrowerService.UpdateStatus(id, ct);

            if(!result)
            {
                return Problem();
            }

            return Ok("Status of user updated");
        }
    }
}
