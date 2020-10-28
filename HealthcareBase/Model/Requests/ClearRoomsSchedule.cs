// File:    ClearRoomsSchedule.cs
// Author:  Lana
// Created: 27 May 2020 20:22:22
// Purpose: Definition of Class ClearRoomsSchedule

using Model.HospitalResources;
using Model.Utilities;
using System;

namespace Model.Requests
{
    public class ClearRoomsSchedule : ScheduleAdjustmentRequest
    {
        private Model.Utilities.TimeInterval timeInterval;
        private Model.HospitalResources.Room room;

        public TimeInterval TimeInterval { get => timeInterval; set => timeInterval = value; }
        public Room Room { get => room; set => room = value; }

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