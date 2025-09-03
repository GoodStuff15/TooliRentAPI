using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ToolType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation properties
        public List<Tool> Tools { get; set; } = new List<Tool>();

        public Category? Category { get; set; } // The category this tool type belongs to - power tools, hand tools, gardening tools, etc.
    }
}
