// File:    ConsiderPatientsProceduresCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:45:48
// Purpose: Definition of Class ConsiderPatientsProceduresCalculator

using Model.Schedule.Procedures;
using Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderPatientsProceduresCalculator : PatientAvailabilityCalculatorDecorator
    {
        private Procedure procedure;

        public ConsiderPatientsProceduresCalculator(Procedure procedure,
            PatientAvailabilityCalculator calculator) : base(calculator)
        {
            this.procedure = procedure;
        }

        public ConsiderPatientsProceduresCalculator(PatientAvailabilityCalculator calculator) : base(calculator)
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
            TimeIntervalCollection newIntervals = new TimeIntervalCollection(patient.Availability.Intervals);

            foreach (TimeInterval timeInterval in patient.Availability.Intervals)
            {
                List<Procedure> conflictingProcedures =
                    context.ProcedureService.GetByPatientAndTime(patient.Patient, timeInterval).ToList();
                if (procedure != null)
                    conflictingProcedures.Remove(procedure);

                foreach (Procedure procedure in conflictingProcedures)
                    newIntervals.SubtractInterval(procedure.TimeInterval);
            }

            return base.Calculate(new PatientAvailabilityDTO { Patient = patient.Patient, Availability = newIntervals }, context);
        }

    }
}