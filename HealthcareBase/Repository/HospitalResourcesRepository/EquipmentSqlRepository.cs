using HealthcareBase.Model.Database;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public class EquipmentSqlRepository : GenericSqlRepository<EquipmentUnit, int>
    {
        public EquipmentSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }

        protected override IQueryable<EquipmentUnit> IncludeFields(IQueryable<EquipmentUnit> query)
        {
            return query.
                Include(unit => unit.CurrentLocation)
                .ThenInclude(location => location.Department)
                .Include(unit => unit.EquipmentType);
        }
    }
}