using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.StatisticsDTOs
{
    public record UserStatisticsDTO
    {
        
        public int TotalUsers { get; init; }

        public int ActiveUsers { get; init; }

        public int InactiveUsers { get; init; }

        public int NewUsersLastMonth { get; init; }

        public int NewUsersLastWeek { get; init; }

        public double LoansPerUser { get; init; }


    }
}
