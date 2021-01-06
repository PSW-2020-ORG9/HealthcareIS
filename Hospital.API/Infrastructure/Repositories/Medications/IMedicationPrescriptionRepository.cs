using General.Repository;
using Hospital.API.Model.Medication;

namespace Hospital.API.Infrastructure.Repositories.Medications
{
    public interface IMedicationPrescriptionRepository : IWrappableRepository<MedicationPrescription, int>
    {
    }
}