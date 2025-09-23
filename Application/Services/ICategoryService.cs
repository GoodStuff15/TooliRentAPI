using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryReadDTO>> GetAllCategories(CancellationToken ct = default);
        Task<CategoryReadDTO?> GetCategoryById(int id, CancellationToken ct = default);
        Task<bool> CreateCategory(CategoryCreateDTO dto, CancellationToken ct = default);
        Task<bool> UpdateCategory(int id, CategoryUpdateDTO dto, CancellationToken ct = default);
        Task<bool> DeleteCategory(int id, CancellationToken ct = default);
    }
}
