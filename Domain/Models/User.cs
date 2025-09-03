using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        // Is this user authorized to borrow?
        public bool isAuthorized { get; set; }  

        // Navigation

        public Borrower? Borrower { get; set; }
        
    }
}
