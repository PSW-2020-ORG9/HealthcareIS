// File:    ConsiderDoctorsShiftsCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:51:02
// Purpose: Definition of Class ConsiderDoctorsShiftsCalculator

using Model.Users.Employee;
using Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderDoctorsShiftsCalculator : DoctorAvailabilityCalculator
    {
        public DoctorAvailabilityDTO Calculate(DoctorAvailabilityDTO doctor, CurrentScheduleContext context)
        {
            IEnumerable<Shift> shifts = context.ShiftService.GetByDoctor(doctor.Doctor);
            TimeIntervalCollection shiftCollection = new TimeIntervalCollection(shifts.Select(shift => shift.TimeInterval));

            return new DoctorAvailabilityDTO
            {
                Doctor = doctor.Doctor,
                Availability = doctor.Availability.Overlap(shiftCollection)
            };
        }

    }
}