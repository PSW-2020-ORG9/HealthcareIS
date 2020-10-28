// File:    DoctorAvailabilityCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:44:09
// Purpose: Definition of Interface DoctorAvailabilityCalculator

namespace Service.ScheduleService.AvailabilityCalculators
{
    public interface DoctorAvailabilityCalculator
    {
        DoctorAvailabilityDTO Calculate(DoctorAvailabilityDTO doctor, CurrentScheduleContext context);
    }
}