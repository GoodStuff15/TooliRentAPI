using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public record RefreshRequestDTO
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
