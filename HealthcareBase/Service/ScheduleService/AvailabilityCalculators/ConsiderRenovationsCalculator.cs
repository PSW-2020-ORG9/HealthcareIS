// File:    ConsiderRenovationsCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:52:22
// Purpose: Definition of Class ConsiderRenovationsCalculator

using Model.Utilities;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderRenovationsCalculator : RoomAvailabilityCalculator
    {
        public RoomAvailabilityDTO Calculate(RoomAvailabilityDTO room, CurrentScheduleContext context)
        {
            var newIntervals = new TimeIntervalCollection(room.Availability.Intervals);

            foreach (var timeInterval in room.Availability.Intervals)
            {
                var conflictingRenovations =
                    context.RenovationService.GetByRoomAndTime(room.Room, timeInterval);
                foreach (var renovation in conflictingRenovations)
                    newIntervals.SubtractInterval(renovation.TimeInterval);
            }

            return new RoomAvailabilityDTO {Room = room.Room, Availability = newIntervals};
        }
    }
}