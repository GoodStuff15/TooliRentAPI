using Domain.DTOs;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ToolTypeService : IToolTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ToolTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ToolTypeReadDTO> CreateAsync(ToolTypeCreateDTO dto, CancellationToken ct = default)
        {
            
            // Mapping logic from ToolTypeCreateDTO to ToolType entity would go here

           
        }

        public Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ToolTypeReadDTO>> GetAllAsync(CancellationToken ct = default)
        {
            var allEntities = await _unitOfWork.ToolTypes.GetAllAsync(includeProperties: "Category");

            // Then mapping logic from ToolType to ToolTypeReadDTO would go here
        }

        public Task<ToolTypeReadDTO?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<ToolTypeReadDTO?> UpdateAsync(int id, ToolTypeUpdateDTO dto, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
