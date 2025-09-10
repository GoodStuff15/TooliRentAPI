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

            foreach(var toolId in dto.ToolIds)
            {
                var tool = await _unitOfWork.Tools.GetByIdAsync(toolId, ct);
                if (tool != null)
                {
                    tool.IsAvailable = false; 
                    toCreate.Tools.Add(tool);

                    await _unitOfWork.Tools.UpdateAsync(tool, ct);
                    await _unitOfWork.SaveChangesAsync(ct);
                }
            }

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

        public async Task<bool> CancelBooking(int id, CancellationToken ct = default)
        {
            var bookingToCancel = await _unitOfWork.Bookings.GetByIdAsync(id, ct);
           
            if (bookingToCancel == null) return false;

            // Mark the booking as cancelled and inactive
            bookingToCancel.IsCancelled = true;
            bookingToCancel.IsActive = false;

            // Make all associated tools available again
            foreach (var tool in bookingToCancel.Tools)
            {
                tool.IsAvailable = true;
                await _unitOfWork.Tools.UpdateAsync(tool, ct);
            }

            await _unitOfWork.Bookings.UpdateAsync(bookingToCancel, ct);

            return await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<bool> CompleteBooking(int id, CancellationToken ct = default)
        {
            var bookingToComplete = await _unitOfWork.Bookings.GetByIdAsync(id, ct);

            if (bookingToComplete == null) return false;

            // Mark the booking as completed and inactive
            bookingToComplete.IsCompleted = true;
            bookingToComplete.IsActive = false;
            bookingToComplete.WasReturned = true;
            bookingToComplete.ReturnedDate = DateOnly.FromDateTime(DateTime.Now);

            // Make all associated tools available again
            foreach (var tool in bookingToComplete.Tools)
            {
                tool.IsAvailable = true;
                await _unitOfWork.Tools.UpdateAsync(tool, ct);
            }

            await _unitOfWork.Bookings.UpdateAsync(bookingToComplete, ct);

            return await _unitOfWork.SaveChangesAsync(ct);
        }
        public async Task<bool> ExtendBooking(int id, DateOnly newEndDate, CancellationToken ct = default)
        {
            var bookingToExtend = await _unitOfWork.Bookings.GetByIdAsync(id, ct);  

            if (bookingToExtend == null) return false;

            // Update the end date
            bookingToExtend.EndDate = newEndDate;
            await _unitOfWork.Bookings.UpdateAsync(bookingToExtend, ct);
            return await _unitOfWork.SaveChangesAsync(ct);

        }

        public async Task<bool> PickupBooking(int id, CancellationToken ct = default)
        {
            var bookingToPickup = await _unitOfWork.Bookings.GetByIdAsync(id, ct);
            if (bookingToPickup == null) return false;

            // Mark the booking as picked up and active, start the rental period
            bookingToPickup.WasPickedUp = true;
            bookingToPickup.IsActive = true;
            bookingToPickup.PickedUpDate = DateOnly.FromDateTime(DateTime.Now);

            await _unitOfWork.Bookings.UpdateAsync(bookingToPickup, ct);

            return await _unitOfWork.SaveChangesAsync(ct);

        }
    }
}
