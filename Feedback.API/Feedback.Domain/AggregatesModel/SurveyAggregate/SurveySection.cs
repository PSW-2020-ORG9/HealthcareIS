using System.Collections.Generic;
using General;

namespace Feedback.API.Model.Survey
{
    public class SurveySection : Entity<int>
    {
        public string SectionName { get; set; }
        public bool IsDoctorSection { get; set; }
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }

        public SurveySection() { }

        public SurveySection(string sectionName, bool isDoctorSection)
        {
            SectionName = sectionName;
            IsDoctorSection = isDoctorSection;
        }
    }
}