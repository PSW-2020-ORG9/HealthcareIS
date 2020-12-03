using System;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using HospitalWebApp.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("available")]
    public class AvailabilityController:ControllerBase
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
            var doctorDtos = AvailableDoctorMapper
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