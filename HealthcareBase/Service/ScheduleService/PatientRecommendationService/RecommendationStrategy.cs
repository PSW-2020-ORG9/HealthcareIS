// File:    RecommendationStrategy.cs
// Author:  Lana
// Created: 02 June 2020 02:58:12
// Purpose: Definition of Interface RecommendationStrategy

using System.Collections.Generic;
using Model.Schedule.Procedures;
using Service.ScheduleService.ScheduleFittingService;

namespace Service.ScheduleService.PatientRecommendationService
{
    public interface RecommendationStrategy
    {
        ProcedureType PatientDefault { get; set; }
        ProcedurePreferenceDTO TransformRequest(RecommendationRequestDTO request);

        Examination ChooseBest(IEnumerable<Examination> potentialRecommendations);
    }
}