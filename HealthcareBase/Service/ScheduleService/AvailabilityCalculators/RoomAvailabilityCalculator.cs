// File:    RoomAvailabilityCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:44:09
// Purpose: Definition of Interface RoomAvailabilityCalculator

namespace HealthcareBase.Service.ScheduleService.AvailabilityCalculators
{
    public interface RoomAvailabilityCalculator
    {
        RoomAvailabilityDTO Calculate(RoomAvailabilityDTO room, CurrentScheduleContext context);
    }
}