using Feedback.API.Model.Survey;

namespace Feedback.API.Services
{
    public interface ISurveyService
    {
        public Survey GetById(int surveyId);
    }
}