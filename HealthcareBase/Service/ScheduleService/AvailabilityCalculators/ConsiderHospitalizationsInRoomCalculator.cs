// File:    ConsiderHospitalizationsInRoomCalculator.cs
// Author:  Lana
// Created: 02 June 2020 11:02:11
// Purpose: Definition of Class ConsiderHospitalizationsInRoomCalculator

using System.Linq;
using Model.Schedule.Hospitalizations;
using Model.Utilities;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderHospitalizationsInRoomCalculator : RoomAvailabilityCalculatorDecorator
    {
        private readonly Hospitalization hospitalization;

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
            var newIntervals = new TimeIntervalCollection(room.Availability.Intervals);

            foreach (var timeInterval in room.Availability.Intervals)
            {
                var conflictingHospizalizations =
                    context.HospitalizationService.GetByRoomAndTime(room.Room, timeInterval).ToList();
                if (hospitalization != null)
                    conflictingHospizalizations.Remove(hospitalization);

                foreach (var hospitalization in conflictingHospizalizations)
                    newIntervals.SubtractInterval(hospitalization.TimeInterval);
            }

            return base.Calculate(new RoomAvailabilityDTO {Room = room.Room, Availability = newIntervals}, context);
        }
    }
}