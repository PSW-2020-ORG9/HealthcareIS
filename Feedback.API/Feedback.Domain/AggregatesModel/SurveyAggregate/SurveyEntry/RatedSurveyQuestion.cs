using Feedback.API.Infrastructure;
using General;
using System.ComponentModel.DataAnnotations;

namespace Feedback.API.Model.Survey.SurveyEntry
{
    public class RatedSurveyQuestion:Entity<int>
    {
        public int SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }

        public const int MinRating = 1;
        public const int MaxRating = 5;
        [Range(MinRating,MaxRating)]
        public int Rating { get; set; }
    }
}