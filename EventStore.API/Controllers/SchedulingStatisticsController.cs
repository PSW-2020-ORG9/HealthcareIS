using EventStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.API.Controllers
{
    [ApiController]
    [Route("event/statistics")]
    public class SchedulingStatisticsController : ControllerBase
    {
        private readonly ISchedulingStatisticsService _schedulingStatisticsService;
        public SchedulingStatisticsController(ISchedulingStatisticsService schedulingStatisticsService)
        {
            _schedulingStatisticsService = schedulingStatisticsService;
        }

        [HttpGet]
        public IActionResult GetStatistics()
        {
            return Ok(_schedulingStatisticsService.GetStatistics());
        }

    }
}
