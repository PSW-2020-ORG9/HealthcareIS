using HealthcareBase.Model.Users.Survey;

namespace HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService
{
    public interface ISurveyService
    {
        public Survey GetById(int surveyId);
    }
}