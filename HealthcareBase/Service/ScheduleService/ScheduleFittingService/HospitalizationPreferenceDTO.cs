// File:    HospitalizationPreferenceDTO.cs
// Author:  Lana
// Created: 02 June 2020 08:50:59
// Purpose: Definition of Class HospitalizationPreferenceDTO

using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Users.Patient;

namespace HealthcareBase.Service.ScheduleService.ScheduleFittingService
{
    public class HospitalizationPreferenceDTO
    {
        public HospitalizationType Type { get; set; }

        public Patient Patient { get; set; }

        public HospitalizationSchedulingPreference Preference { get; set; }
    }
}