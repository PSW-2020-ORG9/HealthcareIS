using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Survey.SurveyEntry
{
    public class RatedSurveySection:Entity<int>
    {
        [Key] public int Id { get; set; }
        [ForeignKey("SurveySection")]
        public int SurveySectionId { get; set; }
        public SurveySection SurveySection { get; set; }
        public IEnumerable<RatedSurveyQuestion> RatedSurveyQuestions { get; set; }
        public int GetKey() => Id;

        public void SetKey(int id) => id = Id;
    }
}