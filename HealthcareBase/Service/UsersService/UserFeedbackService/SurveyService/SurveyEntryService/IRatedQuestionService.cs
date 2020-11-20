using System.Collections.Generic;

namespace Service.UsersService.UserFeedbackService.SurveyService.SurveyEntryService
{
    public interface IRatedQuestionService
    {
        public double GetQuestionAverage(int surveyQuestionId);
        public Dictionary<int, int> GetRatingsCount(int surveyQuestionId);
    }
}