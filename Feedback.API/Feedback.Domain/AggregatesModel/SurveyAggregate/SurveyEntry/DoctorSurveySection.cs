using Feedback.API.Feedback.Domain.AggregatesModel.SurveyAggregate.User;

namespace Feedback.API.Model.Survey.SurveyEntry
{
    public class DoctorSurveySection : RatedSurveySection
    {
        public string DoctorId { get; set; }
    }
}