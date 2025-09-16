using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }

        // Navigation properties

        public int ? ToolTypeId { get; set; } // Foreign Key to ToolType
        public int ? BookingId { get; set; } // Foreign Key to Booking
        public ToolType? ToolType { get; set; } // The type of tool (e.g., drill, saw, etc.)
        public IEnumerable<Booking>? Bookings { get; set; }
        
    }
}
