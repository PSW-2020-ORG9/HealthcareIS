using Feedback.API.Infrastructure;
using Feedback.API.Model.User;
using System;
using System.Collections.Generic;

namespace Feedback.API.Model.Survey.SurveyEntry
{
    public class SurveyResponse : Entity<int>
    {
        public DateTime SubmittedAt { get; set; }
        public int ExaminationId { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public IEnumerable<RatedSurveySection> RatedSurveySections { get; set; }
        public int DoctorSurveySectionId { get; set; }
        public DoctorSurveySection DoctorSurveySection { get; set; }
        public int PatientAccountId { get; set; }
        public PatientAccount PatientAccount { get; set; }
    }
}