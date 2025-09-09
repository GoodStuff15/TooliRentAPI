using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public record BorrowerFilterDTO
    {
        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }

        public bool? IsSuspended { get; init; }

        public DateOnly CreatedAfter { get; init; }
        public DateOnly CreatedBefore { get; init; }

        public bool hasBookings { get; init; }
        public bool hasLateBookings { get; init; }  

    }
}
