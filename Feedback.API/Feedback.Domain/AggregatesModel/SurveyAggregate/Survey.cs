using Feedback.API.DTOs;
using Feedback.API.Model.Survey.SurveyEntry;
using General;
using System;
using System.Collections.Generic;

namespace Feedback.API.Model.Survey
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