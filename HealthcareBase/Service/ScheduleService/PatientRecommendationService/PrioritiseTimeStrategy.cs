// File:    PrioritiseTimeStrategy.cs
// Author:  Lana
// Created: 02 June 2020 02:58:45
// Purpose: Definition of Class PrioritiseTimeStrategy

using System;
using System.Collections.Generic;
using HealthcareBase.Model.Schedule.Procedures;

namespace HealthcareBase.Service.ScheduleService.PatientRecommendationService
{
    public class PrioritiseTimeStrategy : IRecommendationStrategy
    {
        public Examination ChooseBest(Examination examination)
        {
            throw new NotImplementedException();
        }
    }
}