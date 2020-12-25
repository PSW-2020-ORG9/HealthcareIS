using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class SpecialtySqlRepository : GenericSqlRepository<Specialty, int>, ISpecialtyRepository
    {
        public SpecialtySqlRepository(IContextFactory factory) : base(factory)
        {
            
        }
    }
}