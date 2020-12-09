using System.Collections.Generic;
using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Model.Users.Survey.DTOs;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService;
using HospitalWebApp.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurveyController:ControllerBase
    {
        private readonly SurveyPreviewBuilder surveyPreviewBuilder;
        private readonly ISurveyResponseService _surveyResponseService;
        private readonly ISurveyService _surveyService;
        
        public SurveyController(SurveyPreviewBuilder surveyPreviewBuilder, ISurveyResponseService surveyResponseService, ISurveyService surveyService)
        {
            this.surveyPreviewBuilder = surveyPreviewBuilder;
            _surveyResponseService = surveyResponseService;
            _surveyService = surveyService;
        }
        /// <summary>
        /// Gets suitable SurveyDto depending on passed survey id.
        /// </summary>
        /// <param name="id">Survey id</param>
        /// <returns>SurveyDto</returns>
        [HttpGet]
        [Route("preview/admin/{id}")]
        public IActionResult GetAdminPreview(int id)
        {
            return Ok(surveyPreviewBuilder.Build(id));
        }

        [HttpGet]
        [Route("find/{id}")]
        public IActionResult GetSurvey(int id)
        {
            Survey survey = _surveyService.GetById(id);
            if (survey != null) return Ok(survey);
            return BadRequest("Survey with id " + id + " not found.");
        }

        [HttpPost]
        [Route("response")]
        public IActionResult CreateSurveyResponse(SurveyResponseDTO dto)
        {
            return Ok(_surveyResponseService.CreateSurveyResponse(SurveyResponseMapper.DtoToObject(dto)));
        }

        [HttpGet]
        [Route("examination/{examinationId}")]
        public IActionResult GetByExaminationId(int examinationId)
            => Ok(_surveyResponseService.GetByExaminationId(examinationId));

        [HttpPost]
        [Route("examination/multiple")]
        public IActionResult ExistByExaminationIds(List<int> examinationIds)
            => Ok(_surveyResponseService.ExistByExaminationIds(examinationIds));
    }
}