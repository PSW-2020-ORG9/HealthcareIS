// File:    ConsiderDoctorsShiftsCalculator.cs
// Author:  Lana
// Created: 02 June 2020 10:51:02
// Purpose: Definition of Class ConsiderDoctorsShiftsCalculator

using System.Linq;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Service.ScheduleService.AvailabilityCalculators
{
    public class ConsiderDoctorsShiftsCalculator : IDoctorAvailabilityCalculator
    {
        public DoctorAvailabilityDTO Calculate(DoctorAvailabilityDTO doctor, CurrentScheduleContext context)
        {
            var shifts = context.ShiftService.GetByDoctor(doctor.Doctor);
            var shiftCollection = new TimeIntervalCollection(shifts.Select(shift => shift.TimeInterval));

            return new DoctorAvailabilityDTO
            {
                Doctor = doctor.Doctor,
                Availability = doctor.Availability.Overlap(shiftCollection)
            };
        }
    }
}