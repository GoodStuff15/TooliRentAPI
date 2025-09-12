using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerService _borrowerService;

        public BorrowerController(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowerReadDTO>>> GetAllBorrowers(CancellationToken ct)
        {
            var borrowers = await _borrowerService.GetAllAsync();
            return Ok(borrowers);
        }

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

        [HttpPost]
        public async Task<ActionResult<int>> CreateBorrower([FromBody] BorrowerCreateDTO createDto, CancellationToken ct)
        {
            var newBorrowerId = await _borrowerService.CreateAsync(createDto, ct);
            return CreatedAtAction(nameof(GetBorrowerById), new { id = newBorrowerId }, newBorrowerId);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrower(int id, [FromBody] BorrowerUpdateDTO updateDto, CancellationToken ct)
        {
            var result = await _borrowerService.UpdateAsync(id, updateDto, ct);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
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
            return NoContent();
        }
    }
}
