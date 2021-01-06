using Feedback.API.Model.User;

namespace Feedback.API.Model.Survey.SurveyEntry
{
    public class DoctorSurveySection : RatedSurveySection
    {
        public string DoctorId { get; set; }
    }
}