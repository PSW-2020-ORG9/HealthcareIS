using HealthcareBase.Model.Database;
using HealthcareBase.Model.Medication;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MedicationRepository.Interface;
using System.Linq;

namespace HealthcareBase.Repository.MedicationRepository
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
