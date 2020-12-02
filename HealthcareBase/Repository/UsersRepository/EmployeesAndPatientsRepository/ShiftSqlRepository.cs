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
                        .ThenInclude(d=>d.Person);
        }

        public IEnumerable<Shift> GetByDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Shift> GetByDoctorAndTimeContaining(Doctor doctor, TimeInterval time)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Shift> GetByDoctorAndTimeOverlap(Doctor doctor, TimeInterval time)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Shift> GetByShiftStart(DateTime shiftStart)
        {
            return GetMatching(s => s.TimeInterval.Start.Date.Equals(shiftStart.Date));
        }
    }
}