using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Survey.SurveyEntry
{
    public class SurveyResponse:IEntity<int>
    {
        [Key] public int Id { get; set; }
        public DateTime SubmittedAt { get; set; }
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public IEnumerable<RatedSurveySection> RatedSurveySections { get; set; }
        public DoctorSurveySection DoctorSurveySection { get; set; }
        public PatientAccount PatientAccount { get; set; }
        public int GetKey() => Id;

        public void SetKey(int id) => Id = id;
    }
}