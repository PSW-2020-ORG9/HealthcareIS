using Feedback.API.Model.Survey;

namespace Feedback.API.Services.SurveyService
{
    public interface ISurveyService
    {
        public Survey GetById(int surveyId);
    }
}