using Feedback.API.Model.Survey.SurveyEntry;
using System.Collections.Generic;

namespace Feedback.API.DTOs
{
    public class SurveyResponseDTO
    {
        public int SurveyId { get; set; }

        public int PatientAccountId { get; set; }

        public List<RatedSurveySection> RatedSurveySections { get; set; }

        public DoctorSurveySection DoctorSurveySection { get; set; }
        
        public int ExaminationId { get; set; }
    }
}
