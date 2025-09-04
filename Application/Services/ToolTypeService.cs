using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ToolTypeService : IToolTypeService
    {
        public Task<ToolTypeReadDTO> CreateAsync(ToolTypeCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ToolTypeReadDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ToolTypeReadDTO?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ToolTypeReadDTO?> UpdateAsync(int id, ToolTypeUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
