// File:    PatientAvailabilityCalculatorDecorator.cs
// Author:  Lana
// Created: 02 June 2020 11:22:10
// Purpose: Definition of Class PatientAvailabilityCalculatorDecorator

using System;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class PatientAvailabilityCalculatorDecorator : PatientAvailabilityCalculator
    {
        private PatientAvailabilityCalculator patientAvailabilityCalculator;

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
            else
                return patientAvailabilityCalculator.Calculate(patient, context);
        }

    }
}