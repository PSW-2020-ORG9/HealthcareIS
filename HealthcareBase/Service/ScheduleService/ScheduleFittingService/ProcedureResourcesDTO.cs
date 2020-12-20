// File:    ProcedureResourcesDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:17:34
// Purpose: Definition of Class ProcedureResourcesDTO

using System.Collections.Generic;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Service.ScheduleService.ScheduleFittingService
{
    public class ProcedureResourcesDTO
    {
        public ProcedureDetails Details { get; set; }

        public Patient Patient { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; }

        public IEnumerable<Room> Rooms { get; set; }

        public TimeIntervalCollection Timing { get; set; }
    }
}