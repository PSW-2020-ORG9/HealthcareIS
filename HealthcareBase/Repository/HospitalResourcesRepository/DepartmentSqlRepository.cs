using HealthcareBase.Model.Database;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public class DepartmentSqlRepository:GenericSqlRepository<Department,int>,IDepartmentRepository
    {
        public DepartmentSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}