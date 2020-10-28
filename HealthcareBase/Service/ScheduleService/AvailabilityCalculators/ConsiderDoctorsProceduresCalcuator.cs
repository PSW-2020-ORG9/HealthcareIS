// File:    ConsiderDoctorsProceduresCalcuator.cs
// Author:  Lana
// Created: 02 June 2020 10:51:02
// Purpose: Definition of Class ConsiderDoctorsProceduresCalcuator

using System.Linq;
using Model.Schedule.Procedures;
using Model.Utilities;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderDoctorsProceduresCalcuator : DoctorAvailabilityCalculatorDecorator
    {
        private readonly Procedure procedure;

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
            var newIntervals = new TimeIntervalCollection(doctor.Availability.Intervals);

            foreach (var timeInterval in doctor.Availability.Intervals)
            {
                var conflictingProcedures =
                    context.ProcedureService.GetByDoctorAndTime(doctor.Doctor, timeInterval).ToList();
                if (procedure != null)
                    conflictingProcedures.Remove(procedure);

                foreach (var procedure in conflictingProcedures)
                    newIntervals.SubtractInterval(procedure.TimeInterval);
            }

            return base.Calculate(new DoctorAvailabilityDTO {Doctor = doctor.Doctor, Availability = newIntervals},
                context);
        }
    }
}