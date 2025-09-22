using Domain.DTOs;
using Domain.DTOs.ResponseDTOs;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.BusinessValidation
{
    public class Booking_Validation : IBooking_Validation
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBooking_Validation_Response_Builder _responseBuilder;

   
        public Booking_Validation(IUnitOfWork unitOfWork, IBooking_Validation_Response_Builder responseBuilder)
        {
            _unitOfWork = unitOfWork;
            _responseBuilder = responseBuilder;
        }
        public async Task<IEnumerable<int>> AreToolsAvailable(IEnumerable<int> toolIds)
        {
            var unavailableToolIds = new List<int>();
            foreach (var toolId in toolIds)
            {
                var tool = await _unitOfWork.Tools.GetByIdAsync(toolId);
                if (!tool.IsAvailable)
                {
                    unavailableToolIds.Add(toolId);
                }
            }

            return unavailableToolIds;
        }

        public async Task<bool> IsBorrowerActive(int borrowerId)
        {
            var borrower = await _unitOfWork.Borrowers.GetByIdAsync(borrowerId);

            if(borrower == null || !borrower.IsActive)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DoesBookingExist(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            return booking != null;
        }

        public async Task<bool> IsBookingActive(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return false;
            }
            return booking.IsActive;
        }

        public async Task<bool> IsBookingAlreadyReturned(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return false;
            }
            return booking.WasReturned;
        }

        public async Task<bool> IsBookingAlreadyPickedUp(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return false;
            }
            return booking.WasPickedUp;
        }

        public async Task<bool> IsBookingOverdue(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return false;
            }
            return booking.EndDate < DateOnly.FromDateTime(DateTime.Now) && !booking.WasReturned;
        }

        public async Task<bool> IsBookingCompleted(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return false;
            }
            return booking.IsCompleted;
        }

        public async Task<bool> DoesBorrowerExist(int borrowerId)
        {
            var borrower = await _unitOfWork.Borrowers.GetByIdAsync(borrowerId);
            return borrower != null;
        }

        public async Task<bool> DoToolsExist(IEnumerable<int> toolIds)
        {
            foreach (var toolId in toolIds)
            {
                var tool = await _unitOfWork.Tools.GetByIdAsync(toolId);
                if (tool == null)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> IsBookingWithinToolAvailability(IEnumerable<int> toolIds)
        {
            foreach (var toolId in toolIds)
            {
                var tool = await _unitOfWork.Tools.GetByIdAsync(toolId);

                if(tool == null || !tool.IsAvailable)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> CanBookingBeExtended(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);

            if(booking.ExtensionsCount >= Business_Parameters.MaxExtensions)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> IsExtensionWithinRange(int bookingId, DateOnly newEndDate)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);

            if(newEndDate > booking.EndDate.AddDays(Business_Parameters.MaxExtensionDays))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool IsDateRangeValid(DateOnly startDate, DateOnly endDate)
        {
            if(endDate.DayNumber - startDate.DayNumber > Business_Parameters.MaxLoanDays)
            {
                return false; 
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> DoesBorrowerHaveOtherActiveBooking(int borrowerId)
        {
            var activeBookings = await _unitOfWork.Bookings.GetAsync(filter: b => b.BorrowerId == borrowerId && b.IsActive == true);
            return activeBookings.Any();
        }

        public async Task<BookingCreate_ResponseDTO> ValidateCreateBooking(BookingCreateDTO dto)
        {

            return _responseBuilder.DoesBorrowerExist(await DoesBorrowerExist(dto.BorrowerId))
                           .IsBorrowerActive(await IsBorrowerActive(dto.BorrowerId))
                           .AreToolsAvailable(!(await AreToolsAvailable(dto.ToolIds)).Any())
                           .IsDateRangeValid(IsDateRangeValid(dto.StartDate, dto.EndDate))
                           .CreateResponse();
                
        }

        public async Task<BookingUpdate_ResponseDTO> ValidateUpdateBooking(BookingUpdateDTO dto)
        {

            return _responseBuilder.DoesBookingExist(await DoesBookingExist(dto.BookingId))
                           .IsBookingActive(await IsBookingActive(dto.BookingId))
                           .IsBookingAlreadyReturned(await IsBookingAlreadyReturned(dto.BookingId))
                           .IsBookingCancelled(await IsBookingCompleted(dto.BookingId))
                           .UpdateResponse();
        }

        public async Task<BookingUpdate_ResponseDTO> ValidateExtendBooking(int bookingId, DateOnly newEndDate)
        {
            return _responseBuilder.DoesBookingExist(await DoesBookingExist(bookingId))
                           .IsBookingActive(await IsBookingActive(bookingId))
                           .IsBookingAlreadyReturned(await IsBookingAlreadyReturned(bookingId))
                           .IsBookingCancelled(await IsBookingCompleted(bookingId))
                           .CanBookingBeExtended(await CanBookingBeExtended(bookingId))
                           .IsExtensionWithinRange(await IsExtensionWithinRange(bookingId, newEndDate))
                           .UpdateResponse();
        }
    }
}
