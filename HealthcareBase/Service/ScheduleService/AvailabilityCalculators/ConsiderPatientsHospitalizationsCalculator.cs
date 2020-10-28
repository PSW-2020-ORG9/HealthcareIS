// File:    ConsiderPatientsHospitalizationsCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:45:47
// Purpose: Definition of Class ConsiderPatientsHospitalizationsCalculator

using System.Linq;
using Model.Schedule.Hospitalizations;
using Model.Utilities;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderPatientsHospitalizationsCalculator : PatientAvailabilityCalculatorDecorator
    {
        private readonly Hospitalization hospitalization;

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
            var newIntervals = new TimeIntervalCollection(patient.Availability.Intervals);

            foreach (var timeInterval in patient.Availability.Intervals)
            {
                var conflictingHospizalizations =
                    context.HospitalizationService.GetByPatientAndTime(patient.Patient, timeInterval).ToList();
                if (hospitalization != null)
                    conflictingHospizalizations.Remove(hospitalization);
                foreach (var hospitalization in conflictingHospizalizations)
                    newIntervals.SubtractInterval(hospitalization.TimeInterval);
            }

            return base.Calculate(new PatientAvailabilityDTO {Patient = patient.Patient, Availability = newIntervals},
                context);
        }
    }
}