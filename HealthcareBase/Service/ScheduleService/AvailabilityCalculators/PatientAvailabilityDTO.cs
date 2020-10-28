// File:    PatientAvailabilityDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:39:21
// Purpose: Definition of Class PatientAvailabilityDTO

using Model.Users.Patient;
using Model.Utilities;
using System;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class PatientAvailabilityDTO
    {
        private Patient patient;
        private TimeIntervalCollection availability;

        public Patient Patient { get => patient; set => patient = value; }
        public TimeIntervalCollection Availability { get => availability; set => availability = value; }
    }
}