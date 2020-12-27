using Hospital.API.DTOs.Filters;
using Hospital.API.Model.Medication;
using System.Collections.Generic;

namespace Hospital.API.Services.Medications
{
    public interface IMedicationPrescriptionService
    {
        public IEnumerable<MedicationPrescription> SimpleSearch(string nameQuery);
        public IEnumerable<MedicationPrescription> AdvancedSearch(PrescriptionAdvancedFilterDto filterDto);
        public IEnumerable<MedicationPrescription> GetAll();
    }
}
