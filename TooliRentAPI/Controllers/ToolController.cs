using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolController : ControllerBase
    {

        private readonly IToolService _toolService;

        public ToolController(IToolService toolService)
        {
            _toolService = toolService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolReadDTO>>> GetAll(CancellationToken ct = default)
        {
            var result = await _toolService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpGet("FilterSearch")]
        public async Task<ActionResult<IEnumerable<ToolReadDTO>>> GetBySearchFilter([FromQuery] string? nameFilter,
                                                                  int? typeId, int? categoryId, bool? availability,
                                                                  DateOnly? start, DateOnly? end, CancellationToken ct = default)
        {
            var dto = new ToolSearchDTO
            {
                NameFilter = nameFilter,
                TypeId = typeId,
                CategoryId = categoryId,
                Availability = availability,
                StartDate = start,
                EndDate = end
            };


            var filteredTools = await _toolService.GetAllFilteredAsync(dto, ct);
            return Ok(filteredTools);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<int>> CreateTool([FromBody] ToolCreateDTO dto, CancellationToken ct = default)
        {
            var newToolId = await _toolService.CreateAsync(dto, ct);

            // FIX
            return CreatedAtAction(nameof(GetAll), new { id = newToolId }, newToolId);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTool(int id, [FromBody] ToolUpdateDTO dto, CancellationToken ct = default)
        {
            var updateResult = await _toolService.UpdateAsync(id, dto, ct);
            if (!updateResult)
            {
                return NotFound();
            }
            return Ok("Tool updated");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTool(int id, CancellationToken ct = default)
        {
            var deleteResult = await _toolService.DeleteAsync(id, ct);
            if (!deleteResult)
            {
                return NotFound();
            }
            return Ok("Tool deleted");
        }
    }
}
