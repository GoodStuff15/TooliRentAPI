using Domain.DTOs;
using Domain.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.BusinessValidation
{
    public interface IBooking_Validation
    {
        Task<IEnumerable<int>> AreToolsAvailable(IEnumerable<int> toolIds);
        Task<bool> IsBorrowerActive(int borrowerId);
        Task<bool> DoesBookingExist(int bookingId);
        Task<bool> IsBookingActive(int bookingId);
        Task<bool> IsBookingAlreadyReturned(int bookingId);
        Task<bool> IsBookingAlreadyPickedUp(int bookingId);
        Task<bool> IsBookingOverdue(int bookingId);
        Task<bool> IsBookingCompleted(int bookingId);
        Task<bool> DoesBorrowerExist(int borrowerId);
        Task<bool> DoToolsExist(IEnumerable<int> toolIds);
        Task<bool> IsBookingWithinToolAvailability(IEnumerable<int> toolIds);
        Task<bool> CanBookingBeExtended(int bookingId);
        Task<bool> IsExtensionWithinRange(int bookingId, DateOnly newEndDate);
        bool IsDateRangeValid(DateOnly startDate, DateOnly endDate);
        Task<bool> DoesBorrowerHaveOtherActiveBooking(int borrowerId);

        Task<BookingCreate_ResponseDTO> ValidateCreateBooking(BookingCreateDTO dto);
        Task<BookingUpdate_ResponseDTO> ValidateUpdateBooking(BookingUpdateDTO dto);
        Task<BookingUpdate_ResponseDTO> ValidateExtendBooking(int bookingId, DateOnly newEndDate);
    }
}
