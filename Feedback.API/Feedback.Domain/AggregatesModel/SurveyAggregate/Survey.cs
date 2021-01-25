using Feedback.API.Model.Survey.SurveyEntry;
using General;
using System;
using System.Collections.Generic;

namespace Feedback.API.Model.Survey
{
    public class Survey : Entity<int>
    {
        public List<SurveySection> SurveySections { get; set; }

        public List<RatedSurveySection> RatedSurveySections { get; set; }
    }
}