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
    }
}
