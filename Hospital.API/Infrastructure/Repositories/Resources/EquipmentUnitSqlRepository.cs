using General;
using General.Repository;
using Hospital.API.Model.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hospital.API.Infrastructure.Repositories.Resources
{
    public class EquipmentUnitSqlRepository : GenericSqlRepository<EquipmentUnit, int>, IEquipmentUnitRepository
    {
        public EquipmentUnitSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }

        protected override IQueryable<EquipmentUnit> IncludeFields(IQueryable<EquipmentUnit> query)
        {
            return query.
                Include(unit => unit.CurrentLocation)
                    .ThenInclude(location => location.Department)

                .Include(unit => unit.EquipmentType);
        }
    }
}