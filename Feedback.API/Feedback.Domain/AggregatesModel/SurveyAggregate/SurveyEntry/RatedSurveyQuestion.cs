using Feedback.API.Feedback.Domain.AggregatesModel.SurveyAggregate;
using Feedback.API.Infrastructure;
using General;
using System.ComponentModel.DataAnnotations;

namespace Feedback.API.Model.Survey.SurveyEntry
{
    public class RatedSurveyQuestion:Entity<int>
    {
        public int SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }

        public int Rating { get; set; }

        private readonly RateRange RateRange = new RateRange();

        public RatedSurveyQuestion() { }

        public void RateQuestion(int rate)
        {
            RateRange.InRange(rate);
            Rating = rate;
        }

    }
}