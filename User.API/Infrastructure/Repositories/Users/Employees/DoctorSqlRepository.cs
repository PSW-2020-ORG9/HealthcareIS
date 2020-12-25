using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using User.API.Infrastructure.Repositories.Users.Employees.Interfaces;
using User.API.Model.Users.Employees.Doctors;

namespace User.API.Infrastructure.Repositories.Users.Employees
{
    public class DoctorSqlRepository: GenericSqlRepository<Doctor,int>,IDoctorRepository
    {
        public DoctorSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        protected override IQueryable<Doctor> IncludeFields(IQueryable<Doctor> query)
        {
            return query
                .Include(doctor => doctor.Person)
                
                .Include(doctor => doctor.Department)
                
                .Include(doctor => doctor.Specialties)
                .ThenInclude(specialty => specialty.Specialty);
        }

        public IEnumerable<Doctor> GetBySpecialty(Specialty specialty)
        {
            throw new System.NotImplementedException();
        }
    }
}