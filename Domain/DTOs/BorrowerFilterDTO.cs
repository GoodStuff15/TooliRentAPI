using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public record BorrowerFilterDTO
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public bool? IsActive { get; init; }

        //public DateOnly CreatedAfter { get; init; }
        //public DateOnly CreatedBefore { get; init; }

        public bool hasBookings { get; init; }
        public bool hasLateBookings { get; init; }  

    }
}
