// File:    RoomAvailabilityCalculatorDecorator.cs
// Author:  Lana
// Created: 02 June 2020 11:02:36
// Purpose: Definition of Class RoomAvailabilityCalculatorDecorator

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class RoomAvailabilityCalculatorDecorator : RoomAvailabilityCalculator
    {
        private readonly RoomAvailabilityCalculator roomAvailabilityCalculator;

        protected RoomAvailabilityCalculatorDecorator(RoomAvailabilityCalculator roomAvailabilityCalculator)
        {
            this.roomAvailabilityCalculator = roomAvailabilityCalculator;
        }

        protected RoomAvailabilityCalculatorDecorator()
        {
        }

        public RoomAvailabilityDTO Calculate(RoomAvailabilityDTO room, CurrentScheduleContext context)
        {
            if (roomAvailabilityCalculator is null)
                return room;
            return roomAvailabilityCalculator.Calculate(room, context);
        }
    }
}