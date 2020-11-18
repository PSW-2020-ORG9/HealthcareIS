using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Users.UserAccounts;
using Repository.Generics;

namespace HealthcareBase.Model.Users.UserFeedback.Survey
{
    public class SurveyResponse:Entity<int>
    {
        [Key] public int Id { get; set; }
        public DateTime SubmittedAt { get; set; }
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public IEnumerable<RatedSurveySection> RatedSurveySections { get; set; }
        public PatientAccount PatientAccount { get; set; }
        public int GetKey() => Id;

        public void SetKey(int id) => Id = id;
    }
}