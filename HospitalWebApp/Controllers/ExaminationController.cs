using System;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExaminationController : Controller
    {
        private readonly ExaminationService _examinationService;
        
        public ExaminationController(ExaminationService examinationService)
        {
            _examinationService = examinationService;
        }

        [HttpGet]
        [Route("patient/{patientId}")]
        public IActionResult GetExaminationsForPatient(int patientId)
        {
            return Ok(_examinationService.GetByPatientId(patientId));
        }

        [HttpGet]
        [Route("cancel/{examinationId}")]
        public IActionResult Cancel(int examinationId)
        {
            if (_examinationService.Cancel(examinationId))
            {
                return Ok();
            }

            return BadRequest("Failed to cancel examination #" + examinationId);
        }
    }
}