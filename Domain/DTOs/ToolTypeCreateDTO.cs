using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public record ToolTypeCreateDTO
    {
        public string Name { get; init; } = string.Empty;
        public int MinLoanDays { get; init; }
        public int MaxLoanDays { get; init; }
        public int CategoryId { get; init; }
    }
}
