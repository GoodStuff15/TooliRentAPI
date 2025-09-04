using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IToolTypeService
    {
        Task<IEnumerable<ToolTypeReadDTO>> GetAllAsync(CancellationToken ct = default);

        Task<ToolTypeReadDTO?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<ToolTypeReadDTO> CreateAsync(ToolTypeCreateDTO dto, CancellationToken ct = default);

        Task<ToolTypeReadDTO?> UpdateAsync(int id, ToolTypeUpdateDTO dto, CancellationToken ct = default);

        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
