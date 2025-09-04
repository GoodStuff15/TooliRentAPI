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
            await _unitOfWork.SaveChangesAsync(ct);

            return toCreate.Id;


        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entityToDelete = await _unitOfWork.ToolTypes.GetByIdAsync(id, ct);
            if (entityToDelete == null) return false;
            await _unitOfWork.ToolTypes.DeleteAsync(entityToDelete, ct);

            return await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<ToolTypeReadDTO>> GetAllAsync(CancellationToken ct = default)
        {
            var allEntities = await _unitOfWork.ToolTypes.GetAsync(includeProperties: "Category", ct);

            var result = new List<ToolTypeReadDTO>();

            foreach (var tooltype in allEntities)
            {
                result.Add(_mapper.Map<ToolTypeReadDTO>(tooltype));
            }

            return result;
        }

        public async Task<ToolTypeReadDTO?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.ToolTypes.GetAsync
                                          (includeProperties: "Category", ct, i =>  i.Id == id);

            return entity.FirstOrDefault() == null ? null : _mapper.Map<ToolTypeReadDTO>(entity.FirstOrDefault());
        }

        public async Task<bool> UpdateAsync(int id, ToolTypeUpdateDTO dto, CancellationToken ct = default)
        {
            var entityToUpdate = await _unitOfWork.ToolTypes.GetByIdAsync(id, ct);

            if (entityToUpdate == null) return false;
            
            await _unitOfWork.ToolTypes.UpdateAsync(_mapper.Map(dto, entityToUpdate), ct);
            return await _unitOfWork.SaveChangesAsync(ct);

        }
    }
}
