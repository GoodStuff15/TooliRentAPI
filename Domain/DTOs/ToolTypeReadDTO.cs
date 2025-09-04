using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public record ToolTypeReadDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;

        public int MinLoanDays { get; init; }
        public int MaxLoanDays { get; init; }

        public string CategoryName { get; init; } = string.Empty;


    }
}
