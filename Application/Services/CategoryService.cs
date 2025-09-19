using Domain.DTOs;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryReadDTO>> GetAllCategories(CancellationToken ct = default)
        {
            var categories = await _unitOfWork.Categories.GetAsync(ct: ct);
            if (categories == null || !categories.Any())
            {
                return Enumerable.Empty<CategoryReadDTO>();
            }
            return categories.Select(c => new CategoryReadDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
        }
    }
}
