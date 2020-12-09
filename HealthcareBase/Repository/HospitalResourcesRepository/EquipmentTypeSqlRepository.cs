using HealthcareBase.Model.Database;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;
using System.Linq;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public class EquipmentTypeSqlRepository : GenericSqlRepository<EquipmentType, int>, IEquipmentTypeRepository
    {
        public EquipmentTypeSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }

        protected override IQueryable<EquipmentType> IncludeFields(IQueryable<EquipmentType> query)
        {
            return query;
        }
    }
}
