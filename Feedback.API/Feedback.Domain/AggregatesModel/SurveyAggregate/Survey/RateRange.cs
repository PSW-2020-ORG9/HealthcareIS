using Feedback.API.Feedback.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.API.Model.Feedback.Domain.AggregatesModel.SurveyAggregate.Survey
{
    public class RateRange
    {
        public int MinRating { get; }
        public int MaxRating { get; }

        public RateRange(int min,int max) 
        {
            Validate(min, max);
        }

        private void Validate(int min, int max)
        {
            if (max <= min)
                throw new ValidationException(message: $"max rate range must be greater than min");
        }

        public bool InRange(int rate)
        {
            return rate >= MinRating && rate <= MaxRating;
        }
    }
}
