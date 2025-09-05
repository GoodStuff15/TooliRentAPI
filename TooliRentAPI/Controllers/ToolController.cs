using Application.Services;
using Domain.DTOs;
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

        [HttpGet("{toolTypeId}")]
        public async Task<ActionResult<IEnumerable<ToolReadDTO>>> GetByToolTypeId(int toolTypeId, CancellationToken ct = default)
        {
            var allTools = await _toolService.GetAllFilteredAsync(toolTypeId, ct);
            
            return Ok(allTools);
        }
    }
}
