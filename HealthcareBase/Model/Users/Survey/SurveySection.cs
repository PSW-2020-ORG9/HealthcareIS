using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Survey
{
    public class SurveySection : Entity<int>
    {
        public string SectionName { get; set; }
        public bool IsDoctorSection { get; set; }
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }
    }
}