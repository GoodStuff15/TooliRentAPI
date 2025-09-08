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
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<BookingReceiptDTO> CreateBooking(BookingCreateDTO dto, CancellationToken ct = default)
        {
            var toCreate = _mapper.Map<Booking>(dto);

            await _unitOfWork.Bookings.AddAsync(toCreate, ct);

            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<BookingReceiptDTO>(toCreate);
        }

        public async Task<bool> DeleteBooking(int id, CancellationToken ct = default)
        {
            var entityToDelete = await _unitOfWork.Bookings.GetByIdAsync(id, ct);
            if (entityToDelete == null) return false;

            await _unitOfWork.Bookings.DeleteAsync(entityToDelete, ct);

            return await _unitOfWork.SaveChangesAsync(ct);
        }


        public async Task<IEnumerable<BookingReadDTO>> GetAllBookings(CancellationToken ct = default)
        {
           var bookings = await _unitOfWork.Bookings.GetAsync(includeProperties: "Tools", ct: ct);
            var result = new List<BookingReadDTO>();
            foreach(var booking in bookings)
            {
                result.Add(_mapper.Map<BookingReadDTO>(booking));
            }
            return result;
        }

        public async Task<BookingReadDTO?> GetBookingById(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.Bookings.GetAsync(includeProperties: "Tools", ct: ct, b => b.Id == id);

            return entity.FirstOrDefault() == null ? null : _mapper.Map<BookingReadDTO>(entity.First());
        }

        public async Task<IEnumerable<BookingReadDTO>> GetAllUserBookingsAsync(int userId, CancellationToken ct)
        {
            var userBookings = await _unitOfWork.Bookings.GetAsync(filter: b => b.BorrowerId == userId, includeProperties: "Tools", ct: ct);

            var result = new List<BookingReadDTO>();    

            foreach(var booking in userBookings)
            {
              result.Add(_mapper.Map<BookingReadDTO>(booking));
            }

            return result;
        }

        public async Task<bool> UpdateBooking(int id, BookingUpdateDTO dto, CancellationToken ct = default)
        {
            var entityToUpdate = await _unitOfWork.Bookings.GetByIdAsync(id, ct);

            if (entityToUpdate == null) return false;

            await _unitOfWork.Bookings.UpdateAsync(_mapper.Map(dto, entityToUpdate), ct);

            return await _unitOfWork.SaveChangesAsync(ct);
        }

        // NON-CRUD OPERATIONS

        public Task<bool> CancelBooking(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CompleteBooking(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ExtendBooking(int id, DateTime newEndDate, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
