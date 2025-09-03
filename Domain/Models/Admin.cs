using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // Navigation
        
        public Borrower? Borrower { get; set; } 
    }
}
