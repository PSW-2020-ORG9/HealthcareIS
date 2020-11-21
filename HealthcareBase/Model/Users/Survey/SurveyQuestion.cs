using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Survey
{
    public class SurveyQuestion : Entity<int>
    {
        [Key] public int Id { get; set; }
        public string Question { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}