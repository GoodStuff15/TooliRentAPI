using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ResponseDTOs
{
    public class BorrowerCreate_ResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public int BorrowerId { get; set; } // Return the ID of the created borrower
    }
}
