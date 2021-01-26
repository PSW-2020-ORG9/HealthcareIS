using Feedback.API.Feedback.Domain.Exceptions;
using General;
using System;

namespace Feedback.API.Model.Survey.SurveyEntry
{
    public class RatedSurveyQuestion : Entity<int>
    {
        public int Rate { get; }
        public SurveyQuestion Question { get; }

        RatedSurveyQuestion(int rate,SurveyQuestion question)
        {
            Validate(rate, question);
            Rate = rate;
            Question = question;
        }

        private void Validate(int rate, SurveyQuestion question)
        {
            if (!question.RateRange.InRange(rate))
                throw new ValidationException(message: $"Rate is out of range. The question can accept rate between {question.RateRange.MinRating} and {question.RateRange.MaxRating} ");
        }
    }
}