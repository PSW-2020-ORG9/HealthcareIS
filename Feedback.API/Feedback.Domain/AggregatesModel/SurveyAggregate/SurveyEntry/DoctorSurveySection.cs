using Feedback.API.Feedback.Domain.AggregatesModel.SurveyAggregate;
using System.Collections.Generic;

namespace Feedback.API.Model.Survey.SurveyEntry
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