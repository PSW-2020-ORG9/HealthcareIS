// File:    HospitalizationPreferenceDTO.cs
// Author:  Lana
// Created: 02 June 2020 08:50:59
// Purpose: Definition of Class HospitalizationPreferenceDTO

using Model.Schedule.Hospitalizations;
using Model.Schedule.SchedulingPreferences;
using Model.Users.Patient;
using System;

namespace Service.ScheduleService.ScheduleFittingService
{
    public class HospitalizationPreferenceDTO
    {
        private HospitalizationType type;
        private Patient patient;
        private HospitalizationSchedulingPreference preference;

        public HospitalizationType Type { get => type; set => type = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public HospitalizationSchedulingPreference Preference { get => preference; set => preference = value; }
    }
}