// File:    ShiftRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:43:54
// Purpose: Definition of Interface ShiftRepository

using System;
using System.Collections.Generic;
using General.Repository;
using Schedule.API.Model.Shifts;
using Schedule.API.Model.Utilities;

namespace Schedule.API.Infrastructure.Repositories.Shifts
{
    public interface IShiftRepository : IWrappableRepository<Shift, int>
    {
        IEnumerable<Shift> GetByTimeInterval(TimeInterval interval);
        IEnumerable<Shift> GetShiftsByRoomID(int id);

        IEnumerable<Shift> GetByDoctorAndShiftStart(int doctorId, DateTime shiftStart);
        IEnumerable<Shift> GetByShiftStart(DateTime shiftStart);
        public IEnumerable<Shift> GetByDoctorIdAndTimeInterval(int doctorId, TimeInterval interval);
        int GetAssignedRoomId(int doctorId, DateTime date);
    }
}