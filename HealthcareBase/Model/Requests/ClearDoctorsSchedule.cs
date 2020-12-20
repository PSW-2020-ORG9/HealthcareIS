// File:    ClearDoctorsSchedule.cs
// Author:  Lana
// Created: 27 May 2020 20:22:22
// Purpose: Definition of Class ClearDoctorsSchedule

using System;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Model.Requests
{
    public class ClearDoctorsSchedule : ScheduleAdjustmentRequest
    {
        public TimeInterval TimeInterval { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ClearDoctorsSchedule request &&
                   id == request.id;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}