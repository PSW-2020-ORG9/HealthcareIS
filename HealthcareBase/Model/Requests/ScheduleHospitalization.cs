// File:    ScheduleHospitalization.cs
// Author:  Lana
// Created: 27 May 2020 20:27:43
// Purpose: Definition of Class ScheduleHospitalization

using Model.Schedule.Hospitalizations;
using Model.Schedule.SchedulingPreferences;
using Model.Users.Patient;
using System;

namespace Model.Requests
{
    public class ScheduleHospitalization : ScheduleAdjustmentRequest
    {
        private Patient patient;
        private HospitalizationType type;
        private HospitalizationSchedulingPreference preference;

        public Patient Patient { get => patient; set => patient = value; }
        public HospitalizationType Type { get => type; set => type = value; }
        public HospitalizationSchedulingPreference Preference { get => preference; set => preference = value; }

        public override bool Equals(object obj)
        {
            return obj is ScheduleHospitalization request &&
                   id == request.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}