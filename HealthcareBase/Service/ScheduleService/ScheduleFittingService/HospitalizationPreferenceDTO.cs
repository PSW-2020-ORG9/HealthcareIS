// File:    HospitalizationPreferenceDTO.cs
// Author:  Lana
// Created: 02 June 2020 08:50:59
// Purpose: Definition of Class HospitalizationPreferenceDTO

using Model.Schedule.Hospitalizations;
using Model.Schedule.SchedulingPreferences;
using Model.Users.Patient;

namespace Service.ScheduleService.ScheduleFittingService
{
    public class HospitalizationPreferenceDTO
    {
        public HospitalizationType Type { get; set; }

        public Patient Patient { get; set; }

        public HospitalizationSchedulingPreference Preference { get; set; }
    }
}