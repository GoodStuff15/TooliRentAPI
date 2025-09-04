using AutoMapper;
using Domain.DTOs;
using Domain.Models;
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
        private readonly IMapper _mapper;
        public ToolTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(ToolTypeCreateDTO dto, CancellationToken ct = default)
        {

            var toCreate = _mapper.Map<ToolType>(dto);

            await _unitOfWork.ToolTypes.AddAsync(toCreate, ct);
            await _unitOfWork.SaveChanges(ct);

            return toCreate.Id;


        }

        public Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ToolTypeReadDTO>> GetAllAsync(CancellationToken ct = default)
        {
            var allEntities = await _unitOfWork.ToolTypes.GetAllAsync(includeProperties: "Category");

            var result = new List<ToolTypeReadDTO>();

            foreach (var tooltype in allEntities)
            {
                result.Add(_mapper.Map<ToolTypeReadDTO>(tooltype));
            }

            return result;
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
