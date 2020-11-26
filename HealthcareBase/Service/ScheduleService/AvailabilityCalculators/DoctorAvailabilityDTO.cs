// File:    DoctorAvailabilityDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:39:21
// Purpose: Definition of Class DoctorAvailabilityDTO

using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Service.ScheduleService.AvailabilityCalculators
{
    public class DoctorAvailabilityDTO
    {
        public Doctor Doctor { get; set; }

        public TimeIntervalCollection Availability { get; set; }
    }
}