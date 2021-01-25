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

        private readonly List<int> rates;

        public SurveyQuestion(string question,RateRange rateRange, int surveySectionId)
        {
            Validate(question);
            Question = question;
            RateRange = rateRange;
            SurveySectionId = surveySectionId;
            rates = new List<int>();
        }

        public bool Rate(int rate)
        {
            if (RateRange.InRange(rate))
            {
                rates.Add(rate);
                return true;
            }
            return false;
        }

        public float GetAverageRate()
        {
            if (rates.Count == 0)
                throw new Exception(message: "Question has not been rated");
            float sum = 0;
            foreach (int rate in rates)
                sum += rate;
            return sum / rates.Count;
        }

        public float GetAverageRatePercent()
        {
            return GetAverageRate() / RateRange.MaxRating;
        }

        private void Validate(string question)
        {
            if (question.Trim() == string.Empty)
                throw new ValidationException(message: $"question must not be empty");
        }


    }
}