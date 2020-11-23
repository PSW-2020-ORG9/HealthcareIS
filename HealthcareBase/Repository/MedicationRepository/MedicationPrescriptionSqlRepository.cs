using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Database;
using Microsoft.EntityFrameworkCore;
using Model.Medication;
using Repository.Generics;
using Repository.MedicationRepository;

namespace HealthcareBase.Repository.MedicationRepository
{
    public class MedicationPrescriptionSqlRepository 
        : GenericSqlRepository<MedicationPrescription, int>, 
        MedicationPrescriptionRepository
    {
        public MedicationPrescriptionSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
        
        public override IQueryable<MedicationPrescription> IncludeFields(IQueryable<MedicationPrescription> query)
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