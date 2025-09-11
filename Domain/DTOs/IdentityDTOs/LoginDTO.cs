using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.IdentityDTOs
{
    public record LoginDTO
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
