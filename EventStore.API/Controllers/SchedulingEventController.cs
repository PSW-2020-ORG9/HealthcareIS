using System;
using EventStore.API.DTOs;
using EventStore.API.Model.EventStore;
using EventStore.API.Services;
using General.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.API.Controllers
{
    [ApiController]
    [Route("event/scheduling")]
    public class SchedulingEventController : ControllerBase
    {
        private readonly ISchedulingEventService _schedulingEventService;

        public SchedulingEventController(ISchedulingEventService schedulingEventService)
        {
            _schedulingEventService = schedulingEventService;
        }

        [HttpPost]
        public IActionResult Record(SchedulingEventDto schedulingEventDto)
        {
            schedulingEventDto.UserId = Int32.Parse(HttpIdentityHandler.GetUserIdFromRequest(HttpContext.Request));
            return Ok(_schedulingEventService.Record(new SchedulingEvent(schedulingEventDto)));
        }
    }
}