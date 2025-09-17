using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public record ToolReadShorthandDTO
    {
        public string Name { get; init; } = string.Empty;
        
        public string TypeName { get; init; } = string.Empty;
        public string CategoryName { get; init; } = string.Empty;

    }
}
