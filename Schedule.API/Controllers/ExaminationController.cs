﻿using System;
using General.Auth;
using Microsoft.AspNetCore.Mvc;
using Schedule.API.DTOs;
using Schedule.API.Mappers;
using Schedule.API.Model.Exceptions;
using Schedule.API.Model.Filters;
using Schedule.API.Model.Procedures.DTOs;
using Schedule.API.Model.Recommendations;
using Schedule.API.Services.Procedures;
using Schedule.API.Services.Procedures.Interface;

namespace Schedule.API.Controllers
{
    [ApiController]
    [Route("schedule/[controller]")]
    public class ExaminationController : ControllerBase
    {
        private readonly ExaminationServiceProxy _examinationService;
        private readonly RecommendationService _recommendationService;
        private readonly IEquipmentRelocationSchedulingService _equipmentRelocationSchedulingService;


        public ExaminationController(ExaminationServiceProxy examinationService, RecommendationService recommendationService,
            IEquipmentRelocationSchedulingService equipmentRelocationSchedulingService)
        {
            _examinationService = examinationService;
            _recommendationService = recommendationService;
            _equipmentRelocationSchedulingService = equipmentRelocationSchedulingService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            string userId = HttpIdentityHandler.GetUserIdFromRequest(HttpContext.Request);
            return Ok(_examinationService.GetByPatientId(Int32.Parse(userId)));
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
            if (dto.PatientId == 0)
            {
                dto.PatientId = Int32.Parse(HttpIdentityHandler.GetUserIdFromRequest(HttpContext.Request));
            }
            var examination = ExaminationMapper.DtoToObject(dto);
            try
            {
                return Ok(_examinationService.Schedule(examination));
            }
            catch (NullReferenceException)
            {
                return BadRequest("Examination cannot be null.");
            }
            catch (ScheduleViolationException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("recommend")]
        public IActionResult RecommendExamination(RecommendationRequestDto dto)
        {
            return Ok(_recommendationService.Recommend(dto));
        }

        [HttpPost]
        [Route("search/simple")]
        public IActionResult SimpleSearch(ExaminationSimpleFilterDto dto)
        {
            dto.PatientId = Int32.Parse(HttpIdentityHandler.GetUserIdFromRequest(HttpContext.Request));
            return Ok(_examinationService.Search(dto));
        }

        [HttpPost]
        [Route("search/advanced")]
        public IActionResult AdvancedSearch(ExaminationAdvancedFilterDto dto)
        {
            dto.PatientId = Int32.Parse(HttpIdentityHandler.GetUserIdFromRequest(HttpContext.Request));
            return Ok(_examinationService.Search(dto));
        }

        [HttpPost]
        [Route("check-rooms")]
        public IActionResult GetUnavailableRooms(EquipmentRelocationDto dto)
        {
            return Ok(_equipmentRelocationSchedulingService.GetUnavailableRooms(dto));
        }
    }
}