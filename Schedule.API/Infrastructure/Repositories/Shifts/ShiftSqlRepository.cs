using System;
using System.Collections.Generic;
using System.Linq;
using General;
using General.Repository;
using Microsoft.EntityFrameworkCore;
using Schedule.API.Infrastructure.Database;
using Schedule.API.Model.Shifts;
using Schedule.API.Model.Utilities;

namespace Schedule.API.Infrastructure.Repositories.Shifts
{
    public class ShiftSqlRepository:GenericSqlRepository<Shift,int>,IShiftRepository
    {
        public ShiftSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        public IEnumerable<Shift> GetByTimeInterval(TimeInterval interval)
        {
            return GetMatching(shift =>
                shift.TimeInterval.Start.Date >= interval.Start.Date
                && shift.TimeInterval.Start.Date <= interval.End.Date
            );
        }

        public IEnumerable<Shift> GetByDoctorIdAndTimeInterval(int doctorId, TimeInterval interval)
        {
            return GetMatching(shift => 
                shift.DoctorId == doctorId 
                && shift.TimeInterval.Start.Date >= interval.Start.Date
                && shift.TimeInterval.Start.Date <= interval.End.Date
            );
        }
        
        public IEnumerable<Shift> GetByShiftStart(DateTime shiftStart)
        {
            return GetMatching(s => s.TimeInterval.Start.Date.Equals(shiftStart.Date));
        }
        
        public IEnumerable<Shift> GetByDoctorAndShiftStart(int doctorId, DateTime shiftStart)
        {
            return GetMatching(s => s.DoctorId == doctorId
                                    && s.TimeInterval.Start.Date.Equals(shiftStart.Date));
        }

        public int GetAssignedRoomId(int doctorId, DateTime date)
        {
            IEnumerable<Shift> shifts = 
                GetMatching(shift =>
                    shift.DoctorId == doctorId
                    && shift.TimeInterval.Start.Date == date.Date).ToList();

            if (!shifts.Any()) return -1;
            return shifts.First().AssignedExamRoomId;
        }
    }
}