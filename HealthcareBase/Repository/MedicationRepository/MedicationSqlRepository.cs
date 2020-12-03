using HealthcareBase.Model.Database;
using HealthcareBase.Model.Medication;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MedicationRepository.Interface;

namespace HealthcareBase.Repository.MedicationRepository
{
    public class MedicationSqlRepository : GenericSqlRepository<Medication, int>, IMedicationRepository
    {
        public MedicationSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }

    }
}
