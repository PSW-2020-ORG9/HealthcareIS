using HealthcareBase.Model.Users.Survey.SurveyEntry;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthcareBase.Model.Users.Survey.DTOs
{
    public class SurveyResponseDTO
    {
        public int SurveyId { get; set; }

        public int PatientAccountId { get; set; }

        public List<RatedSurveySection> RatedSurveySections { get; set; }

        public DoctorSurveySection DoctorSurveySection { get; set; }
    }
}
