using General;
using General.Repository;
using Hospital.API.Model.Medication;
using System.Linq;

namespace Hospital.API.Infrastructure.Repositories.Medications
{
    public class MedicationSqlRepository : GenericSqlRepository<Medication, int>, IMedicationRepository
    {
        public MedicationSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        protected override IQueryable<Medication> IncludeFields(IQueryable<Medication> query)
        {
            return query;
        }
    }
}
