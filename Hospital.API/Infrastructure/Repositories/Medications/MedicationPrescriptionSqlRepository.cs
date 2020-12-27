using General;
using General.Repository;
using Hospital.API.Model.Medication;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hospital.API.Infrastructure.Repositories.Medications
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
                //.Include(prescription => prescription.Diagnosis)
                //TODO: Fetch from Schedule MS

                .Include(prescription => prescription.Medication)
                .ThenInclude(medication => medication.SideEffects)

                .Include(prescription => prescription.Medication)
                .ThenInclude(medication => medication.Ingredients)

                .Include(prescription => prescription.Instructions);
        }
    }
}