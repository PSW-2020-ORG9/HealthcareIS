// File:    RoomAvailabilityCalculatorDecorator.cs
// Author:  Lana
// Created: 02 June 2020 11:02:36
// Purpose: Definition of Class RoomAvailabilityCalculatorDecorator

using System;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class RoomAvailabilityCalculatorDecorator : RoomAvailabilityCalculator
    {
        private RoomAvailabilityCalculator roomAvailabilityCalculator;

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
            else
                return roomAvailabilityCalculator.Calculate(room, context);
        }

    }
}