// File:    RoomAvailabilityDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:39:22
// Purpose: Definition of Class RoomAvailabilityDTO

using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Service.ScheduleService.AvailabilityCalculators
{
    public class RoomAvailabilityDTO
    {
        public Room Room { get; set; }

        public TimeIntervalCollection Availability { get; set; }
    }
}