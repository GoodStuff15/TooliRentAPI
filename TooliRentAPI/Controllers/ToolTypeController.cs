using Application.Services;
using Domain.DTOs;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolTypeController : ControllerBase
    {
        private readonly IToolTypeService _service;

        public ToolTypeController(IToolTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToolTypes(CancellationToken ct)
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToolTypeReadDTO>> GetToolType(int id, CancellationToken ct)
        {
            var result = await _service.GetByIdAsync(id, ct);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateToolType([FromBody] ToolTypeCreateDTO dto, CancellationToken ct)
        {
            var created = await _service.CreateAsync(dto, ct);
            
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateToolType(int id, [FromBody] ToolTypeUpdateDTO dto, CancellationToken ct)
        {
            var result = await _service.UpdateAsync(id, dto, ct);

            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteToolType(int id, CancellationToken ct)
        {
            var result = await _service.DeleteAsync(id, ct);

            if(!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }


    }
}
