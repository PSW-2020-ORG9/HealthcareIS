using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurveyController:ControllerBase
    {
        private readonly SurveyPreviewBuilder surveyPreviewBuilder;
        
        public SurveyController(SurveyPreviewBuilder surveyPreviewBuilder)
        {
            this.surveyPreviewBuilder = surveyPreviewBuilder;
        }

        [HttpGet]
        [Route("preview/admin/{id}")]
        public IActionResult GetAdminPreview(int id)
        {
            return Ok(surveyPreviewBuilder.Build(id));
        }
    }
}