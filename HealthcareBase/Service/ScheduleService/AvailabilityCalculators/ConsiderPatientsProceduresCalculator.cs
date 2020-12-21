// File:    ConsiderPatientsProceduresCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:45:48
// Purpose: Definition of Class ConsiderPatientsProceduresCalculator

using System.Linq;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderPatientsProceduresCalculator : PatientAvailabilityCalculatorDecorator
    {
        private readonly Procedure procedure;

        public ConsiderPatientsProceduresCalculator(Procedure procedure,
            IPatientAvailabilityCalculator calculator) : base(calculator)
        {
            this.procedure = procedure;
        }

        public ConsiderPatientsProceduresCalculator(IPatientAvailabilityCalculator calculator) : base(calculator)
        {
        }

        public ConsiderPatientsProceduresCalculator(Procedure procedure)
        {
            this.procedure = procedure;
        }

        public ConsiderPatientsProceduresCalculator()
        {
        }

        public new PatientAvailabilityDTO Calculate(PatientAvailabilityDTO patient, CurrentScheduleContext context)
        {
            var newIntervals = new TimeIntervalCollection(patient.Availability.Intervals);

            foreach (var timeInterval in patient.Availability.Intervals)
            {
                var conflictingProcedures =
                    context.ProcedureService.GetByPatientAndTime(patient.Patient, timeInterval).ToList();
                if (procedure != null)
                    conflictingProcedures.Remove(procedure);

                foreach (var oneProcedure in conflictingProcedures)
                    newIntervals.SubtractInterval(oneProcedure.TimeInterval);
            }

            return base.Calculate(new PatientAvailabilityDTO {Patient = patient.Patient, Availability = newIntervals},
                context);
        }
    }
}