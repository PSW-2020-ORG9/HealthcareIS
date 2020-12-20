// File:    ScheduleHospitalization.cs
// Author:  Lana
// Created: 27 May 2020 20:27:43
// Purpose: Definition of Class ScheduleHospitalization

using System;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Users.Patient;

namespace HealthcareBase.Model.Requests
{
    public class ScheduleHospitalization : ScheduleAdjustmentRequest
    {
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [ForeignKey("Type")]
        public int? TypeId { get; set; }
        public HospitalizationType Type { get; set; }

        public HospitalizationSchedulingPreference Preference { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ScheduleHospitalization request &&
                   id == request.id;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}