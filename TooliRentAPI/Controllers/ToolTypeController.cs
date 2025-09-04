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
            var created = _service.CreateAsync(dto, ct);

            if(created == null)
            {
                return 0;
            }

            return Ok(created);
        }


    }
}
