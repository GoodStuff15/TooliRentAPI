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
        Task<IEnumerable<ToolTypeReadDTO>> GetAllAsync();

        Task<ToolTypeReadDTO?> GetByIdAsync(int id);

        Task<ToolTypeReadDTO> CreateAsync(ToolTypeCreateDTO dto);

        Task<ToolTypeReadDTO?> UpdateAsync(int id, ToolTypeUpdateDTO dto);

        Task<bool> DeleteAsync(int id);
    }
}
