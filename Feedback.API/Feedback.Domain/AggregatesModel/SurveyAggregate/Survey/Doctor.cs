using Feedback.API.Model.Feedback.Domain.AggregatesModel.SurveyAggregate.RatedSurvey;
using General;

namespace Feedback.API.Model.Feedback.Domain.AggregatesModel.SurveyAggregate.Survey
{
    public class Doctor : Entity<string>
    {
        public Person Person { get; set; }
    }
}
