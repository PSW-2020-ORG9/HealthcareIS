using HealthcareBase.Model.Database;
using HealthcareBase.Model.Medication;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.MedicationRepository
{
    public class MedicationSqlRepository : GenericSqlRepository<Medication, int>
    {
        public MedicationSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
