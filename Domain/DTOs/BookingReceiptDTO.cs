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
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public DateTime CreatedAt { get; init; }

        public ICollection<ToolReadDTO> BorrowedTools { get; init; } = new List<ToolReadDTO>();
    }
}
