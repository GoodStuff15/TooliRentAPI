using Domain.DTOs;
using Domain.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.BusinessValidation
{
    public interface IBooking_Validation
    {
        public Task<IEnumerable<int>> AreToolsAvailable(IEnumerable<int> toolIds);

        public Task<bool> IsBorrowerActive(int borrowerId);

        public Task<BookingCreate_ResponseDTO> ValidateCreateBooking(BookingCreateDTO dto);

        public Task<BookingUpdate_ResponseDTO> ValidateUpdateBooking(BookingUpdateDTO dto);
    }
}
