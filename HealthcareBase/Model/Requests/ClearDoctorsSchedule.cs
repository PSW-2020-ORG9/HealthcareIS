// File:    ClearDoctorsSchedule.cs
// Author:  Lana
// Created: 27 May 2020 20:22:22
// Purpose: Definition of Class ClearDoctorsSchedule

using Model.Users.Employee;
using Model.Utilities;
using System;

namespace Model.Requests
{
    public class ClearDoctorsSchedule : ScheduleAdjustmentRequest
    {
        private TimeInterval timeInterval;
        private Doctor doctor;

        public TimeInterval TimeInterval { get => timeInterval; set => timeInterval = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }

        public override bool Equals(object obj)
        {
            return obj is ClearDoctorsSchedule request &&
                   id == request.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}