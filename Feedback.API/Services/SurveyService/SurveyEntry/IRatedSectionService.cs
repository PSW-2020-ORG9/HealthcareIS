using System.Collections.Generic;

namespace Feedback.API.Services.SurveyEntry
{
    public interface IRatedSectionService
    {
        public double GetSectionAverage(int surveySectionId);
        public double GetDoctorSectionAverage(int surveySectionId, string doctorId);
        public double GetQuestionAverage(int surveyQuestionId);
        public double GetDoctorQuestionAverage(int surveyQuestionId, string doctorId);
        public Dictionary<int, int> GetRatingsCount(int surveyQuestionId);
        public Dictionary<int, int> GetDoctorsRatingsCount(int surveyQuestionId, string doctorId);
    }
}
