using System;
using Microsoft.AspNetCore.Mvc;
using Schedule.API.Mappers;
using Schedule.API.Services.Procedures;

namespace Schedule.API.Controllers
{
    [ApiController]
    [Route("schedule/available")]
    public class AvailabilityController : ControllerBase
    {
        private readonly DoctorAvailabilityService doctorAvailabilityService;

        public AvailabilityController(DoctorAvailabilityService doctorAvailabilityService)
        {
            this.doctorAvailabilityService = doctorAvailabilityService;
        }
        
        [Route("doctor")]
        [HttpGet]
        public IActionResult GetAvailableDoctors([FromQuery] DateTime date)
        {
            var doctorDtos = DoctorMapper
                .ObjectToDto(doctorAvailabilityService.GetAvailableByDay(date));
            return Ok(doctorDtos);
        }
        
        [Route("interval")]
        [HttpGet]
        public IActionResult GetAvailableIntervals([FromQuery] DateTime date, [FromQuery] int doctorId)
        {
            return Ok(doctorAvailabilityService.GetAvailableIntervals(doctorId, date));
        }
    }
}