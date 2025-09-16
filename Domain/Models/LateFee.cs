using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LateFee
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateOnly DateIncurred { get; set; }

        // Navigation properties

        [ForeignKey("BookingId")]
        public int BookingId { get; set; }

    }
}
