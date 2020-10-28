// File:    ConsiderRenovationsCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:52:22
// Purpose: Definition of Class ConsiderRenovationsCalculator

using Model.HospitalResources;
using Model.Utilities;
using System;
using System.Collections.Generic;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderRenovationsCalculator : RoomAvailabilityCalculator
    {
        public RoomAvailabilityDTO Calculate(RoomAvailabilityDTO room, CurrentScheduleContext context)
        {
            TimeIntervalCollection newIntervals = new TimeIntervalCollection(room.Availability.Intervals);

            foreach (TimeInterval timeInterval in room.Availability.Intervals)
            {
                IEnumerable<Renovation> conflictingRenovations =
                    context.RenovationService.GetByRoomAndTime(room.Room, timeInterval);
                foreach (Renovation renovation in conflictingRenovations)
                    newIntervals.SubtractInterval(renovation.TimeInterval);
            }

            return new RoomAvailabilityDTO { Room = room.Room, Availability = newIntervals };
        }

    }
}