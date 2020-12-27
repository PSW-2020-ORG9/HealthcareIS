using System;
using Microsoft.AspNetCore.Mvc;
using Schedule.API.Mappers;
using Schedule.API.Model.Exceptions;
using Schedule.API.Model.Procedures.DTOs;
using Schedule.API.Model.SchedulingPreferences;
using Schedule.API.Services.Procedures;

namespace Schedule.API.Controllers
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
        public IActionResult ScheduleExamination(ScheduledExaminationDTO dto)
        {
            var examination = ExaminationMapper.DtoToObject(dto);
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

        [HttpPost]
        [Route("recommend")]
        public IActionResult RecommendExamination(RecommendationRequestDto dto)
        {
            return Ok(_examinationService.Recommend(dto));
        }
    }
}