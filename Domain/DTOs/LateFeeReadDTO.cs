using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public record LateFeeReadDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Amount { get; set; }

        public DateOnly DateIncurred { get; set; }

        public bool IsPaid { get; set; }
    }
}
