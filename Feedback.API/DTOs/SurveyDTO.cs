using System.Collections.Generic;

namespace Feedback.API.DTOs
{
    public class SurveyDTO
    {
        public int SurveyId { get; set; }
        public List<SurveySectionDTO> SurveySections { get; set; }
        public List<DoctorSurveySectionDTO> DoctorSurveySections { get; set; }
        
        
    }
}