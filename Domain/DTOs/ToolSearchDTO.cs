using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public record ToolSearchDTO
    {
        public string? NameFilter { get; init; }
        public int? TypeId { get; init; }
        public int? CategoryId { get; init; }
        public bool? Availability { get; init; }

        public DateOnly? StartDate { get; init; }

        public DateOnly? EndDate { get; init; }
    }
}
