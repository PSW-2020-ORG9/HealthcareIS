using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Survey.SurveyEntry
{
    public class RatedSurveyQuestion:IEntity<int>
    {
        [Key]public int Id { get; set; }
        
        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }

        public const int MinRating = 1;
        public const int MaxRating = 5;
        [Range(MinRating,MaxRating)]
        public int Rating { get; set; }
        public int GetKey() => Id;

        public void SetKey(int id) => Id = id;
        
    }
}