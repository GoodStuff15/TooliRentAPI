using Domain.DTOs.StatisticsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IStatisticsService
    {
        Task<UserStatisticsDTO> GetUserStatisticsAsync(CancellationToken ct = default);

        Task<BookingStatisticsDTO> GetBookingStatisticsAsync(CancellationToken ct = default);
    }
}
