// File:    RoomAvailabilityDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:39:22
// Purpose: Definition of Class RoomAvailabilityDTO

using Model.HospitalResources;
using Model.Utilities;
using System;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class RoomAvailabilityDTO
    {
        private Room room;
        private TimeIntervalCollection availability;

        public Room Room { get => room; set => room = value; }
        public TimeIntervalCollection Availability { get => availability; set => availability = value; }
    }
}