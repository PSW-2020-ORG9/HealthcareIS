using EventStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.API.Controllers
{
    [ApiController]
    [Route("statistics")]
    public class SchedulingStatisticsController : ControllerBase
    {
        private readonly ISchedulingStatisticsService _schedulingStatisticsService;
        public SchedulingStatisticsController(ISchedulingStatisticsService schedulingStatisticsService)
        {
            _schedulingStatisticsService = schedulingStatisticsService;
        }

        [HttpGet]
        [Route("age")]
        public IActionResult GetAgeStatistics()
        {
            return Ok(_schedulingStatisticsService.GetAgeStatistics());
        }
    }
}
