using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Survey.SurveyEntry
{
    public class SurveyResponse : IEntity<int>
    {
        [Key] public int Id { get; set; }
        public DateTime SubmittedAt { get; set; }
        
        [ForeignKey("Examination")]
        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }
        
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        
        public IEnumerable<RatedSurveySection> RatedSurveySections { get; set; }
        
        public int DoctorSurveySectionId { get; set; }
        public DoctorSurveySection DoctorSurveySection { get; set; }

        [ForeignKey("PatientAccount")]
        public int PatientAccountId { get; set; }
        public PatientAccount PatientAccount { get; set; }
        
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}