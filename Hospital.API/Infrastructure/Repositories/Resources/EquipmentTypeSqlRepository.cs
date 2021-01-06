using General;
using General.Repository;
using Hospital.API.Model.Resources;
using System.Linq;

namespace Hospital.API.Infrastructure.Repositories.Resources
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
