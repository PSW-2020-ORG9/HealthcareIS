// File:    RecommendationStrategy.cs
// Author:  Lana
// Created: 02 June 2020 02:58:12
// Purpose: Definition of Interface RecommendationStrategy

using Model.Schedule.Procedures;
using Service.ScheduleService.ScheduleFittingService;
using System;
using System.Collections.Generic;

namespace Service.ScheduleService.PatientRecommendationService
{
    public interface RecommendationStrategy
    {
        ProcedurePreferenceDTO TransformRequest(RecommendationRequestDTO request);

        Examination ChooseBest(IEnumerable<Examination> potentialRecommendations);

        ProcedureType PatientDefault { get; set; }

    }
}