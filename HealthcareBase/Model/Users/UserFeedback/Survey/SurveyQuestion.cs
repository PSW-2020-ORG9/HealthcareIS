using System.ComponentModel.DataAnnotations;
using Repository.Generics;

namespace HealthcareBase.Model.Users.UserFeedback.Survey
{
    public class SurveyQuestion : Entity<int>
    {
        [Key] public int Id { get; set; }
        public string Question { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}