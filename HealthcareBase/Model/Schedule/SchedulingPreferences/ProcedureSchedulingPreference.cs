// File:    ProcedureSchedulingPreference.cs
// Author:  Lana
// Created: 21 April 2020 17:32:57
// Purpose: Definition of Class ProcedureSchedulingPreference

using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Utilities;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Model.Schedule.SchedulingPreferences
{
    [Owned]
    public class ProcedureSchedulingPreference
    {
        public TimeIntervalCollection PreferredTime { get; set; }

        [ForeignKey("PreferredDoctor")]
        public int PreferredDoctorId { get; set; }
        public Doctor PreferredDoctor { get; set; }

        [ForeignKey("PreferredRoom")]
        public int PreferredRoomId { get; set; }
        public Room PreferredRoom { get; set; }
    }
}