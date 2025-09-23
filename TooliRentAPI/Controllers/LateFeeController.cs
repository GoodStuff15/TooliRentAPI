using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LateFeeController : ControllerBase
    {

        private readonly ILateFeeService _lateFeeService;
        public LateFeeController(ILateFeeService lateFeeService)
        {
            _lateFeeService = lateFeeService;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LateFeeReadDTO>>> GetAllLateFees(CancellationToken ct = default)
        {
            var lateFees = await _lateFeeService.GetAllLateFees(ct);
            return Ok(lateFees);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<LateFeeReadDTO>>> GetLateFeesByUserId(int userId, CancellationToken ct)
        {
            var lateFees = await _lateFeeService.GetLateFeesByUserId(userId, ct);

            if (!lateFees.Any()) return NotFound("No late fees found for user");

            return Ok(lateFees);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLateFee(int id, CancellationToken ct = default)
        {
            var success = await _lateFeeService.DeleteLateFee(id, ct);
            if (!success) return NotFound("Late fee not found");
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("markAsPaid/{id}")]
        public async Task<IActionResult> MarkLateFeeAsPaid(int id, CancellationToken ct = default)
        {
            var success = await _lateFeeService.MarkLateFeeAsPaid(id, ct);
            if (!success) return NotFound("Late fee not found");
            return Ok("Marked as paid");
        }


    }
}
