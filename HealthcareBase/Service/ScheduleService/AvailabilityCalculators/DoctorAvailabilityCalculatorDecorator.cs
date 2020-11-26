// File:    DoctorAvailabilityCalculatorDecorator.cs
// Author:  Lana
// Created: 02 June 2020 11:15:37
// Purpose: Definition of Class DoctorAvailabilityCalculatorDecorator

namespace HealthcareBase.Service.ScheduleService.AvailabilityCalculators
{
    public class DoctorAvailabilityCalculatorDecorator : DoctorAvailabilityCalculator
    {
        private readonly DoctorAvailabilityCalculator doctorAvailabilityCalculator;

        public DoctorAvailabilityCalculatorDecorator(DoctorAvailabilityCalculator doctorAvailabilityCalculator)
        {
            this.doctorAvailabilityCalculator = doctorAvailabilityCalculator;
        }

        public DoctorAvailabilityCalculatorDecorator()
        {
        }

        public DoctorAvailabilityDTO Calculate(DoctorAvailabilityDTO doctor, CurrentScheduleContext context)
        {
            if (doctorAvailabilityCalculator is null)
                return doctor;
            return doctorAvailabilityCalculator.Calculate(doctor, context);
        }
    }
}