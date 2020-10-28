// File:    PatientAvailabilityCalculatorDecorator.cs
// Author:  Lana
// Created: 02 June 2020 11:22:10
// Purpose: Definition of Class PatientAvailabilityCalculatorDecorator

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class PatientAvailabilityCalculatorDecorator : PatientAvailabilityCalculator
    {
        private readonly PatientAvailabilityCalculator patientAvailabilityCalculator;

        public PatientAvailabilityCalculatorDecorator(PatientAvailabilityCalculator patientAvailabilityCalculator)
        {
            this.patientAvailabilityCalculator = patientAvailabilityCalculator;
        }

        public PatientAvailabilityCalculatorDecorator()
        {
        }

        public PatientAvailabilityDTO Calculate(PatientAvailabilityDTO patient, CurrentScheduleContext context)
        {
            if (patientAvailabilityCalculator is null)
                return patient;
            return patientAvailabilityCalculator.Calculate(patient, context);
        }
    }
}