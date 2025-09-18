using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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


        public async Task<IEnumerable<ToolReadShorthandDTO>> GetAllOverviewAsync(CancellationToken ct = default)
        {
            var allEntities = await _unitOfWork.Tools.GetAsync(includeProperties: "ToolType,ToolType.Category", ct);
            var result = new List<ToolReadShorthandDTO>();
            foreach (var tool in allEntities)
            {
                result.Add(_mapper.Map<ToolReadShorthandDTO>(tool));
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

        public async Task<ToolReadDTO?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var tool = await _unitOfWork.Tools.GetAsync(includeProperties: "ToolType", ct, tool => tool.Id == id);

            return _mapper.Map<ToolReadDTO>(tool.FirstOrDefault());
        }

        public Task<bool> UpdateAsync(int id, ToolUpdateDTO dto, CancellationToken ct = default)
        {
            throw new NotImplementedException(); // FIX
        }

        public Expression<Func<Tool, bool>> FilterFunction(ToolSearchDTO dto)
        {
            return tool => (string.IsNullOrEmpty(dto.NameFilter) || tool.Name.Contains(dto.NameFilter)) &&
                           (!dto.TypeId.HasValue || tool.ToolTypeId == dto.TypeId.Value) &&
                           (!dto.CategoryId.HasValue || tool.ToolType.CategoryId == dto.CategoryId.Value) &&
                           (!dto.Availability.HasValue || tool.IsAvailable == dto.Availability.Value);

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

        public async Task<bool> ChangeAvailability(int id, CancellationToken ct = default)
        {
            var tool = await _unitOfWork.Tools.GetByIdAsync(id, ct);
            if (tool == null) return false;

            if (tool.IsAvailable)
            {
                tool.IsAvailable = false;
            }
            else if (!tool.IsAvailable)
            {
                tool.IsAvailable = true;
            }

            await _unitOfWork.Tools.UpdateAsync(tool);
            return await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}

