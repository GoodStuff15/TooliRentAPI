using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services
{
    public class ToolService : IToolService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ToolService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(ToolCreateDTO dto, CancellationToken ct = default)
        {
            var toCreate = _mapper.Map<Tool>(dto);

            await _unitOfWork.Tools.AddAsync(toCreate, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return toCreate.Id;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entityToDelete = await _unitOfWork.Tools.GetByIdAsync(id, ct);
            if (entityToDelete == null) return false;

            await _unitOfWork.Tools.DeleteAsync(entityToDelete, ct);

            return await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<ToolReadDTO>> GetAllAsync(CancellationToken ct = default)
        {
            var allEntities = await _unitOfWork.Tools.GetAsync(includeProperties: "ToolType", ct);
           
            var result = new List<ToolReadDTO>();

            foreach (var tool in allEntities)
            {
                result.Add(_mapper.Map<ToolReadDTO>(tool));
            }

            return result;
        }

        public async Task<IEnumerable<ToolReadDTO>> GetAllFilteredAsync(ToolSearchDTO dto, CancellationToken ct = default)
        {
            
            var allEntities = await _unitOfWork.Tools.GetAsync(includeProperties: "Bookings,ToolType,ToolType.Category", ct, FilterFunction(dto));
            
            if (dto.StartDate.HasValue && dto.EndDate.HasValue)
            {
                allEntities = allEntities.Where(tool => CheckAvailability(tool, dto)).ToList();
            }

            var result = new List<ToolReadDTO>();
            
            foreach (var tool in allEntities)
            {
                result.Add(_mapper.Map<ToolReadDTO>(tool));
            }
            return result;
        }

        public Task<ToolReadDTO?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, ToolUpdateDTO dto, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<Tool, bool>> FilterFunction(ToolSearchDTO dto)
        {
            return tool => (string.IsNullOrEmpty(dto.NameFilter) || tool.Name.Contains(dto.NameFilter)) &&
                           (!dto.TypeId.HasValue || tool.ToolTypeId == dto.TypeId.Value) &&
                           (!dto.CategoryId.HasValue || tool.ToolType.CategoryId == dto.CategoryId.Value) &&
                           (!dto.Availability.HasValue || tool.IsAvailable == dto.Availability.Value); //&&
                           //(!dto.StartDate.HasValue || tool.Bookings.Any(x => x.StartDate > dto.EndDate)) &&
                           //(!dto.EndDate.HasValue || tool.Bookings.Any(x => x.EndDate < dto.StartDate));

        }

        public bool CheckAvailability(Tool tool, ToolSearchDTO dto)
        {
            foreach (var booking in tool.Bookings)
            {
              
                    // Check if the booking overlaps with the desired date range
                    bool overlaps = booking.StartDate < dto.EndDate && booking.EndDate > dto.StartDate;
                    if (overlaps)
                    {
                        return false; // Tool is not available in the desired date range
                    }
                
                    
                
            }
            return true;
        }
    }
}

