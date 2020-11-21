// File:    HospitalizationResourcesDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:19:22
// Purpose: Definition of Class HospitalizationResourcesDTO

using System.Collections.Generic;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Service.ScheduleService.ScheduleFittingService
{
    public class HospitalizationResourcesDTO
    {
        public HospitalizationType Type { get; set; }

        public Patient Patient { get; set; }

        public IEnumerable<Room> Rooms { get; set; }

        public TimeIntervalCollection Timing { get; set; }
    }
}