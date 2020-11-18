using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Generics;

namespace HealthcareBase.Model.Users.UserFeedback.Survey
{
    public class RatedSurveyQuestion:Entity<int>
    {
        [Key]public int Id { get; set; }
        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
        public int Rating { get; set; }
        public int GetKey() => Id;

        public void SetKey(int id) => Id = id;
    }
}