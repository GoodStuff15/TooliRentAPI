using Domain.DTOs;
using Domain.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IBookingService
    {
        Task<BookingCreate_ResponseDTO> CreateBooking(BookingCreateDTO dto, CancellationToken ct = default);
        Task<IEnumerable<BookingReadDTO>> GetAllBookings(CancellationToken ct = default);
        Task<BookingReadDTO?> GetBookingById(int id, CancellationToken ct = default);
        
        Task<IEnumerable<BookingReadDTO>> GetAllUserBookingsAsync(int userId, CancellationToken ct = default);
        Task<bool> DeleteBooking(int id, CancellationToken ct = default);

        Task<bool> UpdateBooking(int id, BookingUpdateDTO dto, CancellationToken ct = default);

        // NON-CRUD OPERATIONS
        Task<bool> CancelBooking(int id, CancellationToken ct = default);
        Task<bool> CompleteBooking(int id, CancellationToken ct = default);
        Task<bool> ExtendBooking(int id, DateOnly newEndDate, CancellationToken ct = default);

        Task<bool> PickupBooking(int id, CancellationToken ct = default);

        public Task<IEnumerable<BookingReadDTO>> GetLateBookings(bool late, CancellationToken ct = default);

        public Task<bool> UpdateLateBookings(CancellationToken ct = default);

    }
}
