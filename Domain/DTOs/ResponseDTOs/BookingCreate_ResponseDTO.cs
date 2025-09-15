using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ResponseDTOs
{
    public record BookingCreate_ResponseDTO
    {
        public bool Success { get; init; }

        public string Message { get; init; } = string.Empty;

        public int BookingId { get; init; }
        
        public BookingReceiptDTO? BookingDetails { get; init; }

    }
}
