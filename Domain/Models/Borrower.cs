using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Is this an active borrower?
        public bool IsActive { get; set; }

        // Navigation

        public string UserId { get; set; } = string.Empty; // Foreign key

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();


    }
}
