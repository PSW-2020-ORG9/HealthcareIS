// File:    ConsiderDoctorsProceduresCalcuator.cs
// Author:  Lana
// Created: 02 June 2020 10:51:02
// Purpose: Definition of Class ConsiderDoctorsProceduresCalcuator

using System.Linq;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderDoctorsProceduresCalcuator : DoctorAvailabilityCalculatorDecorator
    {
        private readonly Procedure procedure;

        public ConsiderDoctorsProceduresCalcuator(Procedure procedure,
            IDoctorAvailabilityCalculator calculator) : base(calculator)
        {
            this.procedure = procedure;
        }

        public ConsiderDoctorsProceduresCalcuator(IDoctorAvailabilityCalculator calculator) : base(calculator)
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

                foreach (var oneProcedure in conflictingProcedures)
                    newIntervals.SubtractInterval(oneProcedure.TimeInterval);
            }

            return base.Calculate(new DoctorAvailabilityDTO {Doctor = doctor.Doctor, Availability = newIntervals},
                context);
        }
    }
}