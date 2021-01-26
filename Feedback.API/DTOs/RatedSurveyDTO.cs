using Feedback.API.Model.Feedback.Domain.AggregatesModel.SurveyAggregate.RatedSurvey;
using System.Collections.Generic;

namespace Feedback.API.DTOs
{
    public class RatedSurveyDTO
    {
        public int SurveyId { get; set; }

        public int PatientAccountId { get; set; }

        public List<RatedSurveyQuestion> Answers { get; set; }

        
        public int ExaminationId { get; set; }
    }
}
