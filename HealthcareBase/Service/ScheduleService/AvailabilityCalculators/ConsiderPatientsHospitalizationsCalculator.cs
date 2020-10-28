// File:    ConsiderPatientsHospitalizationsCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:45:47
// Purpose: Definition of Class ConsiderPatientsHospitalizationsCalculator

using Model.Schedule.Hospitalizations;
using Model.Schedule.Procedures;
using Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderPatientsHospitalizationsCalculator : PatientAvailabilityCalculatorDecorator
    {
        private Hospitalization hospitalization;

        public ConsiderPatientsHospitalizationsCalculator(Hospitalization hospitalization,
            PatientAvailabilityCalculator calculator) : base(calculator)
        {
            this.hospitalization = hospitalization;
        }

        public ConsiderPatientsHospitalizationsCalculator(PatientAvailabilityCalculator calculator) : base(calculator)
        {
        }

        public ConsiderPatientsHospitalizationsCalculator(Hospitalization hospitalization)
        {
            this.hospitalization = hospitalization;
        }

        public ConsiderPatientsHospitalizationsCalculator()
        {
        }

        public new PatientAvailabilityDTO Calculate(PatientAvailabilityDTO patient, CurrentScheduleContext context)
        {
            TimeIntervalCollection newIntervals = new TimeIntervalCollection(patient.Availability.Intervals);

            foreach (TimeInterval timeInterval in patient.Availability.Intervals)
            {
                List<Hospitalization> conflictingHospizalizations =
                    context.HospitalizationService.GetByPatientAndTime(patient.Patient, timeInterval).ToList();
                if (hospitalization != null)
                    conflictingHospizalizations.Remove(hospitalization);
                foreach (Hospitalization hospitalization in conflictingHospizalizations)
                    newIntervals.SubtractInterval(hospitalization.TimeInterval);
            }

            return base.Calculate(new PatientAvailabilityDTO { Patient = patient.Patient, Availability = newIntervals }, context);
        }

    }
}