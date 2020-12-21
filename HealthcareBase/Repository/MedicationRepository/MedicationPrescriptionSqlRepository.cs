using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Medication;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MedicationRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Repository.MedicationRepository
{
    public class MedicationPrescriptionSqlRepository 
        : GenericSqlRepository<MedicationPrescription, int>, 
        IMedicationPrescriptionRepository
    {
        public MedicationPrescriptionSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
        
        protected override IQueryable<MedicationPrescription> IncludeFields(IQueryable<MedicationPrescription> query)
        {
            return query
                .Include(prescription => prescription.Diagnosis)

                .Include(prescription => prescription.Medication)
                .ThenInclude(medication => medication.SideEffects)

                .Include(prescription => prescription.Medication)
                .ThenInclude(medication => medication.Ingredients)

                .Include(prescription => prescription.Instructions);
        }
    }
}