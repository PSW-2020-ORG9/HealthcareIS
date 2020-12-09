using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class DoctorSqlRepository:GenericSqlRepository<Doctor,int>,IDoctorRepository
    {
        public DoctorSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        protected override IQueryable<Doctor> IncludeFields(IQueryable<Doctor> query)
        {
            return query.Include(d => d.Person)
                .Include(d=>d.Department);
        }

        public IEnumerable<Doctor> GetBySpecialty(Specialty specialty)
        {
            throw new System.NotImplementedException();
        }
    }
}