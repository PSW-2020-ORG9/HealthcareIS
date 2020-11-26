// File:    PrioritiseTimeStrategy.cs
// Author:  Lana
// Created: 02 June 2020 02:58:45
// Purpose: Definition of Class PrioritiseTimeStrategy

using System.Collections.Generic;
using System.Linq;
using Model.Schedule.Procedures;
using Model.Schedule.SchedulingPreferences;
using Model.Utilities;
using Service.ScheduleService.ScheduleFittingService;

namespace Service.ScheduleService.PatientRecommendationService
{
    public class PrioritiseTimeStrategy : RecommendationStrategy
    {
        public ProcedureDetails PatientDefault { get; set; }

        public ProcedurePreferenceDTO TransformRequest(RecommendationRequestDTO request)
        {
            var preference = new ProcedurePreferenceDTO
            {
                Details = PatientDefault,
                Patient = request.Patient,
                Preference = new ProcedureSchedulingPreference
                {
                    PreferredDoctor = null,
                    PreferredTime = new TimeIntervalCollection(request.TimeInterval),
                    PreferredRoom = null
                }
            };
            return preference;
        }

        public Examination ChooseBest(IEnumerable<Examination> potentialRecommendations)
        {
            if (potentialRecommendations.Count() > 0)
                return potentialRecommendations.ToList()[0];
            return null;
        }
    }
}