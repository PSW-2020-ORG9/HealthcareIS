using EventStore.API.DTOs;
using EventStore.API.Model.EventStore.WPFActionEvents;
using EventStore.API.Services.WPFActionEvents;
using General.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Controllers
{
    [ApiController]
    [Route("event/floorchange")]
    public class FloorChangeActionEventController : ControllerBase
    {
        private readonly IFloorChangeActionEventService _floorChangeActionEventService;

        public FloorChangeActionEventController(IFloorChangeActionEventService floorChangeActionEventService)
        {
            _floorChangeActionEventService = floorChangeActionEventService;
        }

        [HttpPost]
        public IActionResult Record(FloorChangeDto floorChangeDto)
        {
            floorChangeDto.UserId = Int32.Parse(HttpIdentityHandler.GetUserIdFromRequest(HttpContext.Request));
            return Ok(_floorChangeActionEventService.Record(new FloorChangeActionEvent(floorChangeDto)));
        }
    }
}
