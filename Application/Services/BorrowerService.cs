using Application.Validators.BusinessValidation;
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

namespace Application.Services
{
    public class BorrowerService : IBorrowerService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Borrower_Validation _validator; 

        public BorrowerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = new Borrower_Validation(_unitOfWork);
        }

        public async Task<int> CreateAsync(BorrowerCreateDTO dto, CancellationToken ct = default)
        {

            // Testing user exists validation

            if (await _validator.DoesUserExistAsync(dto.UserId) == false)
            {
                Console.WriteLine("*********************** NO EXIST **************************");
                return 0;
                
            }

            Console.WriteLine("*********************************YES EXIST **************************");
            var toCreate = _mapper.Map<Borrower>(dto);

            await _unitOfWork.Borrowers.AddAsync(toCreate, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return toCreate.Id;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entityToDelete = await _unitOfWork.Borrowers.GetByIdAsync(id, ct);
            if (entityToDelete == null) return false;

            await _unitOfWork.Borrowers.DeleteAsync(entityToDelete, ct);

            return await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<BorrowerReadDTO>> GetAllAsync(CancellationToken ct = default)
        {
            var allEntities = await _unitOfWork.Borrowers.GetAsync(includeProperties: "Bookings", ct);

            var result = new List<BorrowerReadDTO>();

            foreach (var borrower in allEntities)
            {
                result.Add(_mapper.Map<BorrowerReadDTO>(borrower));
            }

            return result;
        }

        public async Task<BorrowerReadDTO?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.Borrowers.GetAsync(
                filter: b => b.Id == id,
                includeProperties: "Bookings",
                ct: ct);

            return entity.FirstOrDefault() == null ? null : _mapper.Map<BorrowerReadDTO>(entity.FirstOrDefault());
        }

        public async Task<IEnumerable<BorrowerReadDTO>> GetAllFilteredAsync(BorrowerFilterDTO dto, CancellationToken ct = default)
        { 
            var allEntities = await _unitOfWork.Borrowers.GetAsync(includeProperties: "Bookings", ct: ct, filter: FilterFunction(dto));

            var result = new List<BorrowerReadDTO>();

            foreach (var borrower in allEntities)
            {
                result.Add(_mapper.Map<BorrowerReadDTO>(borrower));
            }

            return result;
        }

        public async Task<bool> UpdateAsync(int id, BorrowerUpdateDTO dto, CancellationToken ct = default)
        {
            var entityToUpdate = _unitOfWork.Borrowers.GetByIdAsync(id, ct);

            if (entityToUpdate == null) return false;

            await _unitOfWork.Borrowers.UpdateAsync(_mapper.Map<Borrower>(dto), ct);

            return await _unitOfWork.SaveChangesAsync(ct);
        }

        public Expression<Func<Borrower, bool>> FilterFunction(BorrowerFilterDTO dto)
        {
            return b => (string.IsNullOrEmpty(dto.FirstName) || b.FirstName.Contains(dto.FirstName)) &&
                        (string.IsNullOrEmpty(dto.LastName) || b.LastName.Contains(dto.LastName)) &&
                        (!dto.HasBookings || b.Bookings.Count > 0) &&
                        (!dto.HasLateBookings || b.Bookings.Where(b => b.EndDate < DateOnly.FromDateTime(DateTime.Now))
                                                           .Any());
        }


    }
}
