using Domain.DTOs.StatisticsDTOs;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserStatisticsDTO> GetUserStatisticsAsync(CancellationToken ct = default)
        {
            var users = await _unitOfWork.Borrowers.GetAsync("Bookings",ct);

            if(users == null || !users.Any())
            {
                return null;
            }

            var dto = new UserStatisticsDTO
            {
                TotalUsers = users.Count(),
                ActiveUsers = users.Count(u => u.IsActive),
                InactiveUsers = users.Count(u => !u.IsActive),
                LoansPerUser = users.Average(u => u.Bookings.Count),

            };

            return dto;

        }
        public async Task<BookingStatisticsDTO> GetBookingStatisticsAsync(CancellationToken ct = default)
        {
            var bookings = await _unitOfWork.Bookings.GetAsync(ct: ct);
            if(bookings == null || !bookings.Any())
            {
                return null;
            }

            var dto = new BookingStatisticsDTO
            {
                TotalBookings = bookings.Count(),
                CompletedBookings = bookings.Count(b => b.IsCompleted),
                ActiveBookings = bookings.Count(b => !b.IsActive),
                CancelledBookings = bookings.Count(b => b.IsCancelled),
                NewBookingsLastMonth = bookings.Count(b => b.CreatedAt >= DateTime.UtcNow.AddMonths(-1)),
                NewBookingsLastWeek = bookings.Count(b => b.CreatedAt >= DateTime.UtcNow.AddDays(-7)),
                CurrentLateBookings = bookings.Count(b => b.IsActive && b.EndDate < DateOnly.FromDateTime(DateTime.UtcNow)),
                AverageBookingDurationDays = bookings
                                            .Average(b => (b.EndDate.ToDateTime(new TimeOnly(0, 0, 0)) - b.StartDate.ToDateTime(new TimeOnly(0, 0, 0))).TotalDays)
            };

            return dto;
        }
    }
}
