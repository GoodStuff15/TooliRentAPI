using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToolTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToolTypes()
        {
            var toolTypes = await _unitOfWork.ToolTypes.GetAllAsync(includeProperties: "Category");

            foreach(var x in toolTypes)
            {
                Console.WriteLine(x.Name);
            }
            return Ok(toolTypes);
        }
    }
}
