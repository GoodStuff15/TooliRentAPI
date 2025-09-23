using AutoMapper;
using Domain.DTOs;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LateFeeService : ILateFeeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LateFeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> DeleteLateFee(int id, CancellationToken ct = default)
        {
            var toDelete = await _unitOfWork.LateFees.GetByIdAsync(id);

            if (toDelete == null) return false;

            await _unitOfWork.LateFees.DeleteAsync(toDelete);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }

        public async Task<IEnumerable<LateFeeReadDTO>> GetAllLateFees(CancellationToken ct = default)
        {
            var lateFees = await _unitOfWork.LateFees.GetAsync(ct:ct);

            return _mapper.Map<IEnumerable<LateFeeReadDTO>>(lateFees);
        }

        public async Task<IEnumerable<LateFeeReadDTO>> GetLateFeesByUserId(int id, CancellationToken ct = default)
        {
            var lateFees = await _unitOfWork.LateFees.GetAsync(filter: b => b.UserId == id, ct: ct);

            return  _mapper.Map<IEnumerable<LateFeeReadDTO>>(lateFees);
        }

        public async Task<bool> MarkLateFeeAsPaid(int id, CancellationToken ct = default)
        {
            var toMark = await _unitOfWork.LateFees.GetByIdAsync(id);

            if (toMark == null) return false;

            toMark.IsPaid = true;
            await _unitOfWork.LateFees.UpdateAsync(toMark);
            await _unitOfWork.SaveChangesAsync(ct);

            return true;
        }
    }
}
