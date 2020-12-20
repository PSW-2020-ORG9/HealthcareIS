// File:    RecommendationStrategy.cs
// Author:  Lana
// Created: 02 June 2020 02:58:12
// Purpose: Definition of Interface RecommendationStrategy

using HealthcareBase.Model.Schedule.Procedures;

namespace HealthcareBase.Service.ScheduleService.PatientRecommendationService
{
    public interface IRecommendationStrategy
    {
        Examination ChooseBest(Examination examination);
    }
}