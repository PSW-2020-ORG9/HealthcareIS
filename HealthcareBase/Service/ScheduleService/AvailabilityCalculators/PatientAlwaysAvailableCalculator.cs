// File:    PatientAlwaysAvailableCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:45:48
// Purpose: Definition of Class PatientAlwaysAvailableCalculator

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class PatientAlwaysAvailableCalculator : PatientAvailabilityCalculator
    {
        public PatientAvailabilityDTO Calculate(PatientAvailabilityDTO patient, CurrentScheduleContext context)
        {
            return patient;
        }
    }
}