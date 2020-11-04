// File:    ScheduleHospitalization.cs
// Author:  Lana
// Created: 27 May 2020 20:27:43
// Purpose: Definition of Class ScheduleHospitalization

using Model.Schedule.Hospitalizations;
using Model.Schedule.SchedulingPreferences;
using Model.Users.Patient;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Requests
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
            return 1877310944 + id.GetHashCode();
        }
    }
}