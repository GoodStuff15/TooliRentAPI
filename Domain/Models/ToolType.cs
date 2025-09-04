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

        public int MaxLoanDays { get; set; } // Maximum number of days this type of tool can be loaned out
        public int MinLoanDays { get; set; } // Minimum number of days this type of tool can be loaned out

        // Navigation properties

        public int CategoryId { get; set; } // Foreign key to Category
        public List<Tool> Tools { get; set; } = new List<Tool>();

        public Category? Category { get; set; } // The category this tool type belongs to - power tools, hand tools, gardening tools, etc.
    }
}
