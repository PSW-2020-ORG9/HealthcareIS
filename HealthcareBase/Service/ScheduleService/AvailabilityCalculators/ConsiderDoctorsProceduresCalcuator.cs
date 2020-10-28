// File:    ConsiderDoctorsProceduresCalcuator.cs
// Author:  Lana
// Created: 02 June 2020 10:51:02
// Purpose: Definition of Class ConsiderDoctorsProceduresCalcuator

using Model.Schedule.Procedures;
using Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderDoctorsProceduresCalcuator : DoctorAvailabilityCalculatorDecorator
    {
        private Procedure procedure;

        public ConsiderDoctorsProceduresCalcuator(Procedure procedure,
            DoctorAvailabilityCalculator calculator) : base(calculator)
        {
            this.procedure = procedure;
        }

        public ConsiderDoctorsProceduresCalcuator(DoctorAvailabilityCalculator calculator) : base(calculator)
        {
        }

        public ConsiderDoctorsProceduresCalcuator(Procedure procedure)
        {
            this.procedure = procedure;
        }

        public ConsiderDoctorsProceduresCalcuator()
        {
        }

        public new DoctorAvailabilityDTO Calculate(DoctorAvailabilityDTO doctor, CurrentScheduleContext context)
        {
            TimeIntervalCollection newIntervals = new TimeIntervalCollection(doctor.Availability.Intervals);

            foreach (TimeInterval timeInterval in doctor.Availability.Intervals)
            {
                List<Procedure> conflictingProcedures =
                    context.ProcedureService.GetByDoctorAndTime(doctor.Doctor, timeInterval).ToList();
                if (procedure != null)
                    conflictingProcedures.Remove(procedure);

                foreach (Procedure procedure in conflictingProcedures)
                    newIntervals.SubtractInterval(procedure.TimeInterval);
            }

            return base.Calculate(new DoctorAvailabilityDTO { Doctor = doctor.Doctor, Availability = newIntervals }, context);
        }

    }
}