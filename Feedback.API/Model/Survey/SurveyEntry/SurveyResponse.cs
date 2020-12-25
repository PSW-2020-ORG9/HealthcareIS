using Feedback.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Feedback.API.Model.Survey.SurveyEntry
{
    public class SurveyResponse : Entity<int>
    {
        public DateTime SubmittedAt { get; set; }
        
        [ForeignKey("Examination")]
        public int ExaminationId { get; set; }
        // public Examination Examination { get; set; }
        
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        
        public IEnumerable<RatedSurveySection> RatedSurveySections { get; set; }
        
        public int DoctorSurveySectionId { get; set; }
        public DoctorSurveySection DoctorSurveySection { get; set; }

        [ForeignKey("PatientAccount")]
        public int PatientAccountId { get; set; }
        // public PatientAccount PatientAccount { get; set; }
    }
}