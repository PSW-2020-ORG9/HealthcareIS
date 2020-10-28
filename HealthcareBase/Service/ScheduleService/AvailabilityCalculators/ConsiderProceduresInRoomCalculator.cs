// File:    ConsiderProceduresInRoomCalculator.cs
// Author:  Lana
// Created: 02 June 2020 11:01:57
// Purpose: Definition of Class ConsiderProceduresInRoomCalculator

using Model.Schedule.Procedures;
using Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderProceduresInRoomCalculator : RoomAvailabilityCalculatorDecorator
    {
        private Procedure procedure;

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
            TimeIntervalCollection newIntervals = new TimeIntervalCollection(room.Availability.Intervals);

            foreach (TimeInterval timeInterval in room.Availability.Intervals)
            {
                List<Procedure> conflictingProcedures =
                    context.ProcedureService.GetByRoomAndTime(room.Room, timeInterval).ToList();
                if (procedure != null)
                    conflictingProcedures.Remove(procedure);

                foreach (Procedure procedure in conflictingProcedures)
                    newIntervals.SubtractInterval(procedure.TimeInterval);
            }

            return base.Calculate(new RoomAvailabilityDTO { Room = room.Room, Availability = newIntervals }, context);
        }

    }
}