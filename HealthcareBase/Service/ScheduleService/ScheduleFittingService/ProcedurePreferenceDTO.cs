// File:    ProcedurePreferenceDTO.cs
// Author:  Lana
// Created: 02 June 2020 02:19:59
// Purpose: Definition of Class ProcedurePreferenceDTO

using Model.Schedule.Procedures;
using Model.Schedule.SchedulingPreferences;
using Model.Users.Patient;
using System;

namespace Service.ScheduleService.ScheduleFittingService
{
    public class ProcedurePreferenceDTO
    {
        private ProcedureType type;
        private Patient patient;
        private ProcedureSchedulingPreference preference;

        public ProcedureType Type { get => type; set => type = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public ProcedureSchedulingPreference Preference { get => preference; set => preference = value; }
    }
}