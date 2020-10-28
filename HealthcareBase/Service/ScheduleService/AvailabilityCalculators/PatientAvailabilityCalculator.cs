// File:    PatientAvailabilityCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:43:36
// Purpose: Definition of Interface PatientAvailabilityCalculator

using System;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public interface PatientAvailabilityCalculator
    {
        PatientAvailabilityDTO Calculate(PatientAvailabilityDTO patient, CurrentScheduleContext context);
    }
}