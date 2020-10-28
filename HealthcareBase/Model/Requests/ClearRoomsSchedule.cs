// File:    ClearRoomsSchedule.cs
// Author:  Lana
// Created: 27 May 2020 20:22:22
// Purpose: Definition of Class ClearRoomsSchedule

using Model.HospitalResources;
using Model.Utilities;

namespace Model.Requests
{
    public class ClearRoomsSchedule : ScheduleAdjustmentRequest
    {
        public TimeInterval TimeInterval { get; set; }

        public Room Room { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ClearRoomsSchedule request &&
                   id == request.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}