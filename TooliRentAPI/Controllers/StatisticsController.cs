using Application.Services;
using Domain.DTOs.StatisticsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("UserStatistics")]
        public async Task<ActionResult<UserStatisticsDTO>> GetUserStatistics(CancellationToken ct)
        {

            var result = await _statisticsService.GetUserStatisticsAsync(ct);
        
            if(result == null)
            {
                return NotFound("No user statistics available.");
            }

            return Ok(result);
        }

    }
}
