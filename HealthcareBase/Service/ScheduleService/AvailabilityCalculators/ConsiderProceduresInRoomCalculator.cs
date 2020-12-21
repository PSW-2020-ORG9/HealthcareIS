// File:    ConsiderProceduresInRoomCalculator.cs
// Author:  Lana
// Created: 02 June 2020 11:01:57
// Purpose: Definition of Class ConsiderProceduresInRoomCalculator

using System.Linq;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderProceduresInRoomCalculator : RoomAvailabilityCalculatorDecorator
    {
        private readonly Procedure procedure;

        public ConsiderProceduresInRoomCalculator(Procedure procedure,
            IRoomAvailabilityCalculator calculator) : base(calculator)
        {
            this.procedure = procedure;
        }

        public ConsiderProceduresInRoomCalculator(IRoomAvailabilityCalculator calculator) : base(calculator)
        {
        }

        public ConsiderProceduresInRoomCalculator(Procedure procedure)
        {
            this.procedure = procedure;
        }

        public ConsiderProceduresInRoomCalculator()
        {
        }

        public new RoomAvailabilityDTO Calculate(RoomAvailabilityDTO room, CurrentScheduleContext context)
        {
            var newIntervals = new TimeIntervalCollection(room.Availability.Intervals);

            foreach (var timeInterval in room.Availability.Intervals)
            {
                var conflictingProcedures =
                    context.ProcedureService.GetByRoomAndTime(room.Room, timeInterval).ToList();
                if (procedure != null)
                    conflictingProcedures.Remove(procedure);

                foreach (var oneProcedure in conflictingProcedures)
                    newIntervals.SubtractInterval(oneProcedure.TimeInterval);
            }

            return base.Calculate(new RoomAvailabilityDTO {Room = room.Room, Availability = newIntervals}, context);
        }
    }
}