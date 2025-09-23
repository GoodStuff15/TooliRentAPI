using Application.Services;
using Domain.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryCreateDTO> _createValidator;
        private readonly IValidator<CategoryUpdateDTO> _updateValidator;
        public CategoryController(ICategoryService categoryService, IValidator<CategoryCreateDTO> createValidator, IValidator<CategoryUpdateDTO> updateValidator)
        {
            _categoryService = categoryService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [Authorize(Roles = "Admin, User")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryCreateDTO dto, CancellationToken ct = default)
        {
            var validationResponse = await _createValidator.ValidateAsync(dto, ct);

            if(!validationResponse.IsValid)
            {
                return BadRequest(validationResponse.Errors.Select(e => e.ErrorMessage));
            }

            var creationResult = await _categoryService.CreateCategory(dto, ct);
            if (!creationResult)
            {
                return BadRequest("Category creation failed. It might already exist or validation failed.");
            }
            return Ok("Category created successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDTO dto, CancellationToken ct = default)
        {
            var validationResponse = await _updateValidator.ValidateAsync(dto, ct);
            if(!validationResponse.IsValid)
            {
                return BadRequest(validationResponse.Errors.Select(e => e.ErrorMessage));
            }

            var result = await _categoryService.UpdateCategory(id, dto, ct);

            if(!result)
            {
                return Problem();
            }

            return Ok("Updated");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id, CancellationToken ct = default)
        {
            // Implementation for deleting a category
            return Ok();
        }
    }
}
