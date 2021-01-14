using Feedback.API.Infrastructure;
using General;

namespace Feedback.API.Feedback.Domain.AggregatesModel.SurveyAggregate.User
{
    public class Doctor : Entity<string>
    {
        public Person Person { get; set; }
    }
}
