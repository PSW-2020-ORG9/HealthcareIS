using System.Collections.Generic;

namespace HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService.SurveyEntryService
{
    public interface IRatedSectionService
    {
        public double GetSectionAverage(int surveySectionId);
        public double GetDoctorSectionAverage(int surveySectionId, int doctorId);
        public double GetQuestionAverage(int surveyQuestionId);
        public double GetDoctorQuestionAverage(int surveyQuestionId, int doctorId);
        public Dictionary<int, int> GetRatingsCount(int surveyQuestionId);
        public Dictionary<int, int> GetDoctorsRatingsCount(int surveyQuestionId, int doctorId);
    }
}
