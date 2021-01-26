using General;
using System;
using System.Collections.Generic;

namespace Feedback.API.Model.Feedback.Domain.AggregatesModel.SurveyAggregate.Survey
{
    public class Survey : Entity<int>
    {
        public List<SurveySection> SurveySections { get; private set; }

        public Survey(List<SurveySection> surveySections)
        {
            SurveySections = surveySections;
        }

    }
}