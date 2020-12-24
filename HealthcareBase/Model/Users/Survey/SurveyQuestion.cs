using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Survey
{
    public class SurveyQuestion : Entity<int>
    {
        public string Question { get; set; }
    }
}