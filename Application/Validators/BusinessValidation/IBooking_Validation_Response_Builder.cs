using Domain.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.BusinessValidation
{
    public interface IBooking_Validation_Response_Builder
    {
        Booking_Validation_Response_Builder DoesBorrowerExist(bool exists);
        Booking_Validation_Response_Builder IsBorrowerActive(bool isActive);
        Booking_Validation_Response_Builder AreToolsAvailable(bool available);
        Booking_Validation_Response_Builder IsDateRangeValid(bool isValid);

        Booking_Validation_Response_Builder DoesBookingExist(bool exists);
        Booking_Validation_Response_Builder IsBookingActive(bool isActive);
        Booking_Validation_Response_Builder IsBookingAlreadyReturned(bool alreadyReturned);
        Booking_Validation_Response_Builder IsBookingCancelled(bool isCancelled);

        BookingCreate_ResponseDTO CreateResponse();
        BookingUpdate_ResponseDTO UpdateResponse();
    }
}
