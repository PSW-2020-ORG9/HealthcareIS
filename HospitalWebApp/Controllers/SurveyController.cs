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
        
        public SurveyController(SurveyPreviewBuilder surveyPreviewBuilder, ISurveyResponseService surveyResponseService)
        {
            this.surveyPreviewBuilder = surveyPreviewBuilder;
            _surveyResponseService = surveyResponseService;
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

        [HttpPost]
        [Route("survey-response")]
        public IActionResult CreateSurveyResponse(SurveyResponseDTO dto)
        {
            return Ok(_surveyResponseService.CreateSurveyResponse(SurveyResponseMapper.DtoToObject(dto)));
        }
    }
}