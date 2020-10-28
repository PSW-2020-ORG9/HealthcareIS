// File:    ProcedureResourcesDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:17:34
// Purpose: Definition of Class ProcedureResourcesDTO

using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;
using System;
using System.Collections.Generic;

namespace Service.ScheduleService.ScheduleFittingService
{
    public class ProcedureResourcesDTO
    {
        private ProcedureType type;
        private Patient patient;
        private IEnumerable<Doctor> doctors;
        private IEnumerable<Room> rooms;
        private TimeIntervalCollection timing;

        public ProcedureType Type { get => type; set => type = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public IEnumerable<Doctor> Doctors { get => doctors; set => doctors = value; }
        public IEnumerable<Room> Rooms { get => rooms; set => rooms = value; }
        public TimeIntervalCollection Timing { get => timing; set => timing = value; }
    }
}