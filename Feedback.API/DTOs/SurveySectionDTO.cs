using System.Collections.Generic;

namespace Feedback.API.DTOs
{
    public class SurveySectionDTO
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public double AverageRating { get; set; }
        public List<SurveyQuestionDTO> SurveyQuestions { get; set; }
    }
}