using Feedback.API.Infrastructure;
using General;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feedback.API.Model.Survey.SurveyEntry
{
    public class RatedSurveySection : Entity<int>
    {
        public int SurveySectionId { get; set; }
        public SurveySection SurveySection { get; set; }
        public IEnumerable<RatedSurveyQuestion> RatedSurveyQuestions { get; set; }
    }
}