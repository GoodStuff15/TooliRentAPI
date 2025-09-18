using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public record ToolReadShorthandDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;

        public bool IsAvailable { get; init; }
        
        public string TypeName { get; init; } = string.Empty;
        public string CategoryName { get; init; } = string.Empty;

    }
}
