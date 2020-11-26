// File:    PrioritiseDoctorStrategy.cs
// Author:  Lana
// Created: 02 June 2020 02:58:44
// Purpose: Definition of Class PrioritiseDoctorStrategy

using System.Collections.Generic;
using System.Linq;
using Model.Schedule.Procedures;
using Model.Schedule.SchedulingPreferences;
using Service.ScheduleService.ScheduleFittingService;

namespace Service.ScheduleService.PatientRecommendationService
{
    public class PrioritiseDoctorStrategy : RecommendationStrategy
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
                    PreferredDoctor = request.Doctor,
                    PreferredTime = null,
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