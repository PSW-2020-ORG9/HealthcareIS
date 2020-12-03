using System;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Schedule.Procedures.DTOs;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using HospitalWebApp.Mappers;
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

        [HttpPost]
        public IActionResult ScheduleExamination(ScheduledExaminationDTO scheduledExaminationDto)
        {
            var examination = ExaminationMapper.DtoToObject(scheduledExaminationDto);
            try
            {
                examination = _examinationService.Schedule(examination);
            }
            catch (NullReferenceException)
            {
                return BadRequest("Examination cannot be null.");
            }
            catch (ScheduleViolationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(examination);

        }
    }
}