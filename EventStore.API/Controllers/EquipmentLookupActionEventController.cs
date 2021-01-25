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
    [Route("event/equipmentlookup")]
    public class EquipmentLookupActionEventController : ControllerBase
    {
        private readonly IEquipmentLookupActionEventService _equipmentLookupActionEventService;

        public EquipmentLookupActionEventController(IEquipmentLookupActionEventService equipmentLookupActionEventService)
        {
            _equipmentLookupActionEventService = equipmentLookupActionEventService;
        }

        [HttpPost]
        public IActionResult Record(EquipmentLookupDto equipmentLookupDto)
        {
            equipmentLookupDto.UserId = Int32.Parse(HttpIdentityHandler.GetUserIdFromRequest(HttpContext.Request));
            return Ok(_equipmentLookupActionEventService.Record(new EquipmentLookupActionEvent(equipmentLookupDto)));
        }
    }
}
