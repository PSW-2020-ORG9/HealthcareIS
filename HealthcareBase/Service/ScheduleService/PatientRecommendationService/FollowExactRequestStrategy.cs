// File:    FollowExactRequestStrategy.cs
// Author:  Lana
// Created: 02 June 2020 02:58:44
// Purpose: Definition of Class FollowExactRequestStrategy

using Model.Schedule.Procedures;
using Model.Schedule.SchedulingPreferences;
using Model.Utilities;
using Service.ScheduleService.ScheduleFittingService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ScheduleService.PatientRecommendationService
{
    public class FollowExactRequestStrategy : RecommendationStrategy
    {
        private ProcedureType patientDefault;

        public ProcedureType PatientDefault { get => patientDefault; set => patientDefault = value; }

        public ProcedurePreferenceDTO TransformRequest(RecommendationRequestDTO request)
        {
            ProcedurePreferenceDTO preference = new ProcedurePreferenceDTO()
            {
                Type = PatientDefault,
                Patient = request.Patient,
                Preference = new ProcedureSchedulingPreference()
                {
                    PreferredDoctor = request.Doctor,
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
            else
                return null;
        }

    }
}