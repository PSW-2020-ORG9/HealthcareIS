using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Generics;

namespace HealthcareBase.Model.Users.UserFeedback.Survey
{
    public class SurveySection:Entity<int>
    {
        [Key]public int Id { get; set; }
        public string SectionName { get; set; }
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}