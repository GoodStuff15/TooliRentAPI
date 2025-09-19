using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDTO>>> GetAllCategories(CancellationToken ct = default)
        {
            var result = await _categoryService.GetAllCategories(ct);
            if (result == null || !result.Any())
            {
                return NotFound("No categories found.");
            }
            return Ok(result);
        }
    }
}
