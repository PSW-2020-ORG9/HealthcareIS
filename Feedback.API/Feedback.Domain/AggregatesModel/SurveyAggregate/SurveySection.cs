using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Feedback.API.Infrastructure;
using General;

namespace Feedback.API.Model.Survey
{
    public class SurveySection : Entity<int>
    {
        public string SectionName { get; set; }
        public bool IsDoctorSection { get; set; }
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }
    }
}