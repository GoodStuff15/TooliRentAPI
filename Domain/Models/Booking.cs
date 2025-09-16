using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Booking
    {
        public int Id { get; set; }

        // The requested Start and End of the booking
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }


        // The actual Pick up and Return date

        public DateOnly? PickedUpDate { get; set; }

        public DateOnly? ReturnedDate { get; set; }

        // When was the booking created?
        public DateTime CreatedAt { get; set; }

        // Is it currently active?
        public bool IsActive { get; set; } = false;

        // Was it picked up?
        public bool WasPickedUp { get; set; } = false;

        // Was it returned?
        public bool WasReturned { get; set; } = false;

        // Is it completed?
        public bool IsCompleted { get; set; } = false;

        // Was it cancelled?
        public bool IsCancelled { get; set; } = false;


        // Navigation properties

      
        public int BorrowerId { get; set; } // Foreign Key to Borrower

        public ICollection<Tool> Tools { get; set; } = new List<Tool>();

        public int? LateFeeId { get; set; }
    }
}
