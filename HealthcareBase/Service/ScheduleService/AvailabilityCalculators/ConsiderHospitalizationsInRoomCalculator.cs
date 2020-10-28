// File:    ConsiderHospitalizationsInRoomCalculator.cs
// Author:  Lana
// Created: 02 June 2020 11:02:11
// Purpose: Definition of Class ConsiderHospitalizationsInRoomCalculator

using Model.Schedule.Hospitalizations;
using Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderHospitalizationsInRoomCalculator : RoomAvailabilityCalculatorDecorator
    {
        private Hospitalization hospitalization;

        public ConsiderHospitalizationsInRoomCalculator(Hospitalization hospitalization,
            RoomAvailabilityCalculator calculator) : base(calculator)
        {
            this.hospitalization = hospitalization;
        }

        public ConsiderHospitalizationsInRoomCalculator(Hospitalization hospitalization)
        {
            this.hospitalization = hospitalization;
        }

        public ConsiderHospitalizationsInRoomCalculator(RoomAvailabilityCalculator calculator) : base(calculator)
        {
        }

        public ConsiderHospitalizationsInRoomCalculator()
        {
        }

        public new RoomAvailabilityDTO Calculate(RoomAvailabilityDTO room, CurrentScheduleContext context)
        {
            TimeIntervalCollection newIntervals = new TimeIntervalCollection(room.Availability.Intervals);

            foreach (TimeInterval timeInterval in room.Availability.Intervals)
            {
                List<Hospitalization> conflictingHospizalizations =
                    context.HospitalizationService.GetByRoomAndTime(room.Room, timeInterval).ToList();
                if (hospitalization != null)
                    conflictingHospizalizations.Remove(hospitalization);

                foreach (Hospitalization hospitalization in conflictingHospizalizations)
                    newIntervals.SubtractInterval(hospitalization.TimeInterval);
            }

            return base.Calculate(new RoomAvailabilityDTO { Room = room.Room, Availability = newIntervals }, context);
        }

    }
}