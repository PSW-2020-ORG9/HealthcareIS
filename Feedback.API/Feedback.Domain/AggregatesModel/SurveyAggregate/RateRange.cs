using Feedback.API.Feedback.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.API.Feedback.Domain.AggregatesModel.SurveyAggregate
{
    public class RateRange
    {
        public const int MinRating = 1;
        public const int MaxRating = 5;

        public RateRange() { }

        public void InRange(int rate)
        {
            if (!(rate >= MinRating && rate <= MaxRating))
                throw new ValidationException(message: $"Rate must be between {MinRating} and {MaxRating}.");
        }
    }
}
