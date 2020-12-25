using User.API.Infrastructure.Repositories.Users.Employees.Interfaces;
using User.API.Model.Users.Employees.Doctors;

namespace User.API.Infrastructure.Repositories.Users.Employees
{
    public class SpecialtySqlRepository : GenericSqlRepository<Specialty, int>, ISpecialtyRepository
    {
        public SpecialtySqlRepository(IContextFactory factory) : base(factory)
        {
        }
    }
}