// File:    DoctorAvailabilityDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:39:21
// Purpose: Definition of Class DoctorAvailabilityDTO

using Model.Users.Employee;
using Model.Utilities;
using System;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class DoctorAvailabilityDTO
    {
        private Doctor doctor;
        private TimeIntervalCollection availability;

        public Doctor Doctor { get => doctor; set => doctor = value; }
        public TimeIntervalCollection Availability { get => availability; set => availability = value; }
    }
}