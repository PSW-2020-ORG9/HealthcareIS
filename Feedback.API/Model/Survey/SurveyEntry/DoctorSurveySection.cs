using System.ComponentModel.DataAnnotations.Schema;


namespace Feedback.API.Model.Survey.SurveyEntry
{
    public class DoctorSurveySection : RatedSurveySection
    {
        public Doctor Doctor { get; set; }
    }
}