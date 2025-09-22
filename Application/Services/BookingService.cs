using Application.Validators.BusinessValidation;
using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.ResponseDTOs;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBooking_Validation _validator;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper, IBooking_Validation validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }


        public async Task<BookingCreate_ResponseDTO> CreateBooking(BookingCreateDTO dto, CancellationToken ct = default)
        {
            var validationResponse = await _validator.ValidateCreateBooking(dto);

            if (!validationResponse.Success)
            {
                return validationResponse;
            }

            var toCreate = _mapper.Map<Booking>(dto);

            foreach (var toolId in dto.ToolIds)
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
            
            validationResponse.BookingDetails = _mapper.Map<BookingReceiptDTO>(toCreate);
            validationResponse.BookingId = toCreate.Id;

            return validationResponse;
            
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
        public async Task<BookingUpdate_ResponseDTO> ExtendBooking(int id, DateOnly newEndDate, CancellationToken ct = default)
        {
            var bookingToExtend = await _unitOfWork.Bookings.GetByIdAsync(id, ct);  

            var validationResponse = await _validator.ValidateExtendBooking(bookingToExtend.Id, newEndDate);

            if(!validationResponse.Success)
            {
                return validationResponse;
            }

            // Update the end date
            bookingToExtend.EndDate = newEndDate;
            validationResponse.Message = "Booking successfully extended.";
            await _unitOfWork.Bookings.UpdateAsync(bookingToExtend, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return validationResponse; 

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

        public async Task<IEnumerable<BookingReadDTO>> GetLateBookings(bool late, CancellationToken ct = default)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            IEnumerable<Booking> bookings;
            if (late)
            {
                bookings = await _unitOfWork.Bookings.GetAsync(filter: b => b.EndDate < today && b.IsActive && !b.WasReturned && b.WasPickedUp, includeProperties: "Tools", ct: ct);
            }
            else
            {
                bookings = await _unitOfWork.Bookings.GetAsync(filter: b => b.EndDate >= today || !b.IsActive, includeProperties: "Tools", ct: ct);
            }
            var result = new List<BookingReadDTO>();
            foreach(var booking in bookings)
            {
                result.Add(_mapper.Map<BookingReadDTO>(booking));
            }
            return result;
        }

        // Run this method daily via a scheduled job to update late bookings and apply/increment late fees
        public async Task<bool> UpdateLateBookings(CancellationToken ct = default)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var lateBookings = await _unitOfWork.Bookings.GetAsync(filter: b => b.EndDate < today && b.IsActive && !b.WasReturned && b.WasPickedUp, includeProperties: "Tools", ct: ct);
            foreach(var booking in lateBookings)
            {
                if(booking.LateFeeId == null)
                {
                    // Create a new late fee record if it doesn't exist
                    var newLateFee = new LateFee
                    {
                        BookingId = booking.Id,
                        Amount = Business_Parameters.LateFee, // Initial late fee amount
                        DateIncurred = DateOnly.FromDateTime(DateTime.Now)
                    };
                    await _unitOfWork.LateFees.AddAsync(newLateFee, ct);
                    await _unitOfWork.SaveChangesAsync(ct); // Save to get the new LateFee ID
                    booking.LateFeeId = newLateFee.Id;
                    await _unitOfWork.Bookings.UpdateAsync(booking, ct);
                    await _unitOfWork.SaveChangesAsync(ct);
                }
                else
                {   // If a late fee record exists, increment the amount
                    var existingFee = await _unitOfWork.LateFees.GetByIdAsync((int)booking.LateFeeId, ct);
                    if (existingFee != null)
                    {
                        existingFee.Amount += Business_Parameters.LateFee; // Increment by $10 for each day late
                        await _unitOfWork.LateFees.UpdateAsync(existingFee, ct);
                        await _unitOfWork.SaveChangesAsync(ct);
                    }
                }
                
                
            }
            return await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
