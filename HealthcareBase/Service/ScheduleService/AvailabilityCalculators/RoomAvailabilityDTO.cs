// File:    RoomAvailabilityDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:39:22
// Purpose: Definition of Class RoomAvailabilityDTO

using Model.HospitalResources;
using Model.Utilities;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class RoomAvailabilityDTO
    {
        public Room Room { get; set; }

        public TimeIntervalCollection Availability { get; set; }
    }
}