// File:    ProcedureResourcesDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:17:34
// Purpose: Definition of Class ProcedureResourcesDTO

using System.Collections.Generic;
using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;

namespace Service.ScheduleService.ScheduleFittingService
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