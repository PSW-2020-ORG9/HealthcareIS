// File:    PatientAvailabilityCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:43:36
// Purpose: Definition of Interface PatientAvailabilityCalculator

namespace HealthcareBase.Service.ScheduleService.AvailabilityCalculators
{
    public interface IPatientAvailabilityCalculator
    {
        PatientAvailabilityDTO Calculate(PatientAvailabilityDTO patient, CurrentScheduleContext context);
    }
}