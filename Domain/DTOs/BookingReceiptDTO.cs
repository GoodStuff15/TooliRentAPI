using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public record BookingReceiptDTO
    {
        public int BookingId { get; init; }
        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }
        public DateTime CreatedAt { get; init; }

        public ICollection<ToolReadDTO> BorrowedTools { get; init; } = new List<ToolReadDTO>();
    }
}
