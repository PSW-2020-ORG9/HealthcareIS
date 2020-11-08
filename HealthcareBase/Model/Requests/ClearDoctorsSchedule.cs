// File:    ClearDoctorsSchedule.cs
// Author:  Lana
// Created: 27 May 2020 20:22:22
// Purpose: Definition of Class ClearDoctorsSchedule

using Model.Users.Employee;
using Model.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Requests
{
    public class ClearDoctorsSchedule : ScheduleAdjustmentRequest
    {
        public TimeInterval TimeInterval { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

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