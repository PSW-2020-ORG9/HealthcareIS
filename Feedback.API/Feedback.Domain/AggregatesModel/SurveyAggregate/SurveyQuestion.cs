using General;

namespace Feedback.API.Model.Survey
{
    public class SurveyQuestion : Entity<int>
    {
        public string Question { get; set; }
    }
}