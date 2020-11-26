// File:    PatientAvailabilityDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:39:21
// Purpose: Definition of Class PatientAvailabilityDTO

using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Service.ScheduleService.AvailabilityCalculators
{
    public class PatientAvailabilityDTO
    {
        public Patient Patient { get; set; }

        public TimeIntervalCollection Availability { get; set; }
    }
}