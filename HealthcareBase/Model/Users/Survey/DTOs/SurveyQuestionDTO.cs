using System.Collections.Generic;

namespace HealthcareBase.Model.Users.Survey.DTOs
{
    public class SurveyQuestionDTO
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public double QuestionAverage { get; set; }
        public Dictionary<int,int> RatingsCount { get; set; }
    }
}