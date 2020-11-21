using System.Collections.Generic;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class DoctorSqlRepository:GenericSqlRepository<Doctor,int>,DoctorRepository
    {
        public DoctorSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        public IEnumerable<Doctor> GetBySpecialty(Specialty specialty)
        {
            throw new System.NotImplementedException();
        }
    }
}