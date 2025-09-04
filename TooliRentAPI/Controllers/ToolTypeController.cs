using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolTypeController : ControllerBase
    {


        public ToolTypeController()
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToolTypes()
        {

            return Ok();
        }
    }
}
