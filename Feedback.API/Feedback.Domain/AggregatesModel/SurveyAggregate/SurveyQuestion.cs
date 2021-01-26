using Feedback.API.Feedback.Domain.AggregatesModel.SurveyAggregate;
using General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Feedback.API.Model.Survey
{
    public class SurveyQuestion : Entity<int>
    {
        public string Question { get; }
        public RateRange RateRange { get; }

        public int SurveySectionId { get; }


        public SurveyQuestion(string question,RateRange rateRange, int surveySectionId)
        {
            Validate(question);
            Question = question;
            RateRange = rateRange;
            SurveySectionId = surveySectionId;
        }

        private void Validate(string question)
        {
            if (question.Trim() == string.Empty)
                throw new ValidationException(message: $"question must not be empty");
        }


    }
}