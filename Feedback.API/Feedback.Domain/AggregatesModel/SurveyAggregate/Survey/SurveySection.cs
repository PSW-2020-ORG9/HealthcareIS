using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using General;

namespace Feedback.API.Model.Feedback.Domain.AggregatesModel.SurveyAggregate.Survey
{
    public class SurveySection : Entity<int>
    {
        public string SectionName { get; set; }
        public SectionType SectionType { get; protected set; }
        public List<SurveyQuestion> SurveyQuestions { get; }

        public SurveySection(string sectionName, List<SurveyQuestion> surveyQuestions)
        {
            Validate(sectionName, surveyQuestions.Count);
            SectionName = sectionName;
            SectionType = SectionType.Regular;
            SurveyQuestions = surveyQuestions;
        }

        private void Validate(string sectionName, int count)
        {
            if (sectionName.Trim() == string.Empty)
                throw new ValidationException(message: $"section name must not be empty");
            if (count == 0)
                throw new ValidationException(message: $"section needs at least one question");
        }


    }
}