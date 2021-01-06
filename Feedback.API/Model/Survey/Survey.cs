// File:    PatientSurveyResponse.cs
// Author:  Lana
// Created: 21 April 2020 18:23:22
// Purpose: Definition of Class PatientSurveyResponse

using Feedback.API.Infrastructure;
using General;
using System.Collections.Generic;

namespace Feedback.API.Model.Survey
{
    public class Survey : Entity<int>
    {
        public List<SurveySection> SurveySections { get; set; }
    }
}