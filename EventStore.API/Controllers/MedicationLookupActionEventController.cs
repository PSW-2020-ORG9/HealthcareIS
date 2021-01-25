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
    [Route("event/medicationlookup")]
    public class MedicationLookupActionEventController : ControllerBase
    {
        private readonly IMedicationLookupActionEventService _medicationLookupActionEventService;

        public MedicationLookupActionEventController(IMedicationLookupActionEventService medicationLookupActionEventService)
        {
            _medicationLookupActionEventService = medicationLookupActionEventService;
        }

        [HttpPost]
        public IActionResult Record(MedicationLookupDto medicationLookupDto)
        {
            medicationLookupDto.UserId = Int32.Parse(HttpIdentityHandler.GetUserIdFromRequest(HttpContext.Request));
            return Ok(_medicationLookupActionEventService.Record(new MedicationLookupActionEvent(medicationLookupDto)));
        }
    }
}
