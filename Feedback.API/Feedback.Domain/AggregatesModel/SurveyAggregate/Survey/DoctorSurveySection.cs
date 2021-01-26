using System.Collections.Generic;

namespace Feedback.API.Model.Feedback.Domain.AggregatesModel.SurveyAggregate.Survey
{
    public class DoctorSurveySection : SurveySection
    {
        public int DoctorId { get; }
        public DoctorSurveySection(string sectionName, List<SurveyQuestion> surveyQuestions, int doctorId): base(sectionName, surveyQuestions)
        {
            DoctorId = doctorId;
            base.SectionType=SectionType.Doctor;
        }

    }
}