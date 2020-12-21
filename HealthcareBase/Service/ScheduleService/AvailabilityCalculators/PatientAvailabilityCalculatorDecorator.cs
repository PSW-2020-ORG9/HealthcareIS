// File:    PatientAvailabilityCalculatorDecorator.cs
// Author:  Lana
// Created: 02 June 2020 11:22:10
// Purpose: Definition of Class PatientAvailabilityCalculatorDecorator

namespace HealthcareBase.Service.ScheduleService.AvailabilityCalculators
{
    public class PatientAvailabilityCalculatorDecorator : IPatientAvailabilityCalculator
    {
        private readonly IPatientAvailabilityCalculator patientAvailabilityCalculator;

        public PatientAvailabilityCalculatorDecorator(IPatientAvailabilityCalculator patientAvailabilityCalculator)
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