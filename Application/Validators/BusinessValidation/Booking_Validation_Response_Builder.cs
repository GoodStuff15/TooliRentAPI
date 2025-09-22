using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.ResponseDTOs;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.BusinessValidation
{
    public class Booking_Validation_Response_Builder : IBooking_Validation_Response_Builder
    {
        private BookingCreate_ResponseDTO _createResponse;
        private BookingUpdate_ResponseDTO _updateResponse;

        private readonly IMapper _mapper;


        public Booking_Validation_Response_Builder(IMapper mapper)
        {
            _createResponse = new BookingCreate_ResponseDTO();
            _updateResponse = new BookingUpdate_ResponseDTO();
            _mapper = mapper;
        }

        public Booking_Validation_Response_Builder AreToolsAvailable(bool yes)
        {
            
            return this;
        }

        public Booking_Validation_Response_Builder IsBorrowerActive(bool yes)
        {

            if (!yes)
            {
                _createResponse.Success = false;
                _createResponse.Message += "The borrower is not active.\n ";
            }

            return this;
        }

        public Booking_Validation_Response_Builder DoesBookingExist(bool yes)
        {
  
            if (!yes)
            {
                _updateResponse.Success = false;
                _updateResponse.Message = "The booking does not exist.\n ";
            }
            return this;
        }

        public Booking_Validation_Response_Builder IsBookingActive(bool yes)
        {
            if(!yes)
            {

                _updateResponse.Success = false;
                _updateResponse.Message += "The booking is not active.\n ";
            }

            return this;

        }

        public Booking_Validation_Response_Builder IsBookingAlreadyReturned(bool yes)
        {
           if(!yes)
            {
                _updateResponse.Success = false;
                _updateResponse.Message += "The booking has already been returned.\n ";
            }
            return this;
        }

        public Booking_Validation_Response_Builder IsBookingAlreadyPickedUp(bool yes)
        {
            if(!yes)
            {
                _updateResponse.Success = false;
                _updateResponse.Message += "The booking has already been picked up.\n ";
            }
            return this;
        }

        public Booking_Validation_Response_Builder IsBookingCancelled(bool yes)
        {
           if(!yes)
            {
                _updateResponse.Success = false;
                _updateResponse.Message += "The booking has been cancelled.\n ";
            }
            return this;
        }

        public Booking_Validation_Response_Builder IsBookingCompleted(bool yes)
        {
            if(!yes)
            {
                _updateResponse.Success = false;
                _updateResponse.Message += "The booking has been completed.\n ";
            }
            return this;
        }

        public Booking_Validation_Response_Builder IsToolListValid(bool yes)
        {
           if(!yes)
            {
                _createResponse.Success = false;
                _createResponse.Message += "One or more tools in the booking do not exist or are unavailable.\n ";
            }
            return this;
        }

        public Booking_Validation_Response_Builder IsDateRangeValid(bool yes)
        {
            if(!yes)
            {
                _createResponse.Success = false;
                _createResponse.Message += "The date range for the booking is invalid.\n ";
            }   
            return this;
        }

        public Booking_Validation_Response_Builder DoesBorrowerExist(bool yes)
        {
            if(!yes)
            {
                _createResponse.Success = false;
                _createResponse.Message += "The borrower does not exist.\n ";
            }   
            return this;
        }

        public Booking_Validation_Response_Builder DoToolsExist(bool yes)
        {
            if(!yes)
            {
                _createResponse.Success = false;
                _createResponse.Message += "One or more tools in the booking do not exist.\n ";
            }
            return this;
        }

        public Booking_Validation_Response_Builder IsBookingWithinToolAvailability(bool yes)
        {
            if(!yes)
            {
                _createResponse.Success = false;
                _createResponse.Message += "One or more tools in the booking are not available for the entire booking period.\n ";
            }   
            return this;
        }

        public Booking_Validation_Response_Builder CanBookingBeExtended(bool yes)
        {
            if(!yes)
            {
                _updateResponse.Success = false;
                _updateResponse.Message += "The booking cannot be extended further.\n ";
            }
            return this;
        }

        public Booking_Validation_Response_Builder IsExtensionWithinRange(bool yes)
        {
            if(!yes)
            {
                _updateResponse.Success = false;
                _updateResponse.Message += "The extension date is outside the allowable range.\n ";
            }
            return this;
        }


        public BookingCreate_ResponseDTO CreateResponse()
            { return _createResponse; }
        public BookingUpdate_ResponseDTO UpdateResponse()
            { return _updateResponse; }
    }
}
