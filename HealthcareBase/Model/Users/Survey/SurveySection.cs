using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Survey
{
    public class SurveySection:Entity<int>
    {
        [Key]public int Id { get; set; }
        public string SectionName { get; set; }
        public bool IsDoctorSection { get; set; }
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}