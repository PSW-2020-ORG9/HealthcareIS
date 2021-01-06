using General;
using General.Repository;
using Hospital.API.Model.Resources;

namespace Hospital.API.Infrastructure.Repositories.Resources
{
    public class DepartmentSqlRepository : GenericSqlRepository<Department, int>, IDepartmentRepository
    {
        public DepartmentSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}