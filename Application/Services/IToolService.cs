using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IToolService
    {
        Task<IEnumerable<ToolReadDTO>> GetAllAsync(CancellationToken ct = default);

        Task<IEnumerable<ToolReadDTO>> GetAllFilteredAsync(int toolTypeId, CancellationToken ct = default);

        Task<ToolReadDTO?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(ToolCreateDTO dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, ToolUpdateDTO dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);

    }
}
