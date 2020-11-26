using System.Collections.Generic;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class DoctorSqlRepository:GenericSqlRepository<Doctor,int>,IDoctorRepository
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