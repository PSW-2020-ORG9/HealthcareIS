using System.Collections.Generic;
using Feedback.API.DTOs;
using Feedback.API.Mappers;
using Feedback.API.Model.Survey;
using Feedback.API.Services;
using Feedback.API.Services.SurveyService;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.API.Controllers
{
    [ApiController]
    [Route("feedbacks/[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly SurveyPreviewBuilder _surveyPreviewBuilder;
        private readonly ISurveyResponseService _surveyResponseService;
        private readonly ISurveyService _surveyService;
        
        public SurveyController(SurveyPreviewBuilder surveyPreviewBuilder, ISurveyResponseService surveyResponseService, ISurveyService surveyService)
        {
            _surveyPreviewBuilder = surveyPreviewBuilder;
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
            return Ok(_surveyPreviewBuilder.Build(id));
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