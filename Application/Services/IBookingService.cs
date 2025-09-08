using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IBookingService
    {
        Task<BookingReceiptDTO> CreateBooking(BookingCreateDTO dto, CancellationToken ct = default);
        Task<IEnumerable<BookingReadDTO>> GetAllBookings(CancellationToken ct = default);
        Task<BookingReadDTO?> GetBookingById(int id, CancellationToken ct = default);
          Task<bool> DeleteBooking(int id, CancellationToken ct = default);

        Task<bool> UpdateBooking(int id, BookingUpdateDTO dto, CancellationToken ct = default);

        // NON-CRUD OPERATIONS
        // Are these useful?
        Task<bool> CancelBooking(int id, CancellationToken ct = default);
        Task<bool> CompleteBooking(int id, CancellationToken ct = default);
        Task<bool> ExtendBooking(int id, DateTime newEndDate, CancellationToken ct = default);


    }
}
