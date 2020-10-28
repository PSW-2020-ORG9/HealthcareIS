// File:    ConsiderProceduresInRoomCalculator.cs
// Author:  Lana
// Created: 02 June 2020 11:01:57
// Purpose: Definition of Class ConsiderProceduresInRoomCalculator

using System.Linq;
using Model.Schedule.Procedures;
using Model.Utilities;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderProceduresInRoomCalculator : RoomAvailabilityCalculatorDecorator
    {
        private readonly Procedure procedure;

        public ConsiderProceduresInRoomCalculator(Procedure procedure,
            RoomAvailabilityCalculator calculator) : base(calculator)
        {
            this.procedure = procedure;
        }

        public ConsiderProceduresInRoomCalculator(RoomAvailabilityCalculator calculator) : base(calculator)
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

                foreach (var procedure in conflictingProcedures)
                    newIntervals.SubtractInterval(procedure.TimeInterval);
            }

            return base.Calculate(new RoomAvailabilityDTO {Room = room.Room, Availability = newIntervals}, context);
        }
    }
}