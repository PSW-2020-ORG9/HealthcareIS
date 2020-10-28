// File:    HospitalizationResourcesDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:19:22
// Purpose: Definition of Class HospitalizationResourcesDTO

using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using Model.Users.Patient;
using Model.Utilities;
using System;
using System.Collections.Generic;

namespace Service.ScheduleService.ScheduleFittingService
{
    public class HospitalizationResourcesDTO
    {
        private HospitalizationType type;
        private Patient patient;
        private IEnumerable<Room> rooms;
        private TimeIntervalCollection timing;

        public HospitalizationType Type { get => type; set => type = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public IEnumerable<Room> Rooms { get => rooms; set => rooms = value; }
        public TimeIntervalCollection Timing { get => timing; set => timing = value; }
    }
}