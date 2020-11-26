// File:    ProcedurePreferenceDTO.cs
// Author:  Lana
// Created: 02 June 2020 02:19:59
// Purpose: Definition of Class ProcedurePreferenceDTO

using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Users.Patient;

namespace HealthcareBase.Service.ScheduleService.ScheduleFittingService
{
    public class ProcedurePreferenceDTO
    {
        public ProcedureType Type { get; set; }

        public Patient Patient { get; set; }

        public ProcedureSchedulingPreference Preference { get; set; }
    }
}