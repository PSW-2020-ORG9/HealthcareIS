using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class ShiftSqlRepository:GenericSqlRepository<Shift,int>,IShiftRepository
    {
        public ShiftSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        protected override IQueryable<Shift> IncludeFields(IQueryable<Shift> query)
        {
            return query.Include(s => s.Doctor)
                .ThenInclude(d => d.Person)
                .Include(d => d.Doctor)
                .ThenInclude(d => d.Department);
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