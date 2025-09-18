using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.StatisticsDTOs
{
    public record BookingStatisticsDTO
    {
        public int TotalBookings { get; init; }
        public int ActiveBookings { get; init; }
        public int CancelledBookings { get; init; }
        public int CompletedBookings { get; init; }
        public int CurrentLateBookings { get; init; }
        public int NewBookingsLastMonth { get; init; }
        public int NewBookingsLastWeek { get; init; }
        public double BookingsPerUser { get; init; }

        public double AverageBookingDurationDays { get; init; }
    }
}
