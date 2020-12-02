// File:    RecommendationStrategy.cs
// Author:  Lana
// Created: 02 June 2020 02:58:12
// Purpose: Definition of Interface RecommendationStrategy

using System.Collections.Generic;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Service.ScheduleService.ScheduleFittingService;

namespace HealthcareBase.Service.ScheduleService.PatientRecommendationService
{
    public interface IRecommendationStrategy
    {
        Examination ChooseBest(Examination examination);
    }
}