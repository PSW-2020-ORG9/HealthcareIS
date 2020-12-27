using General.Repository;
using Hospital.API.DTOs.Filters;
using Hospital.API.Infrastructure.Repositories.Medications;
using Hospital.API.Model.Medication;
using System.Collections.Generic;

namespace Hospital.API.Services.Medications
{
    public class MedicationPrescriptionService : IMedicationPrescriptionService
    {
        private readonly RepositoryWrapper<IMedicationPrescriptionRepository> _medicationPrescriptionWrapper;

        public MedicationPrescriptionService(
            IMedicationPrescriptionRepository medicationPrescriptionRepository
        )
        {
            this._medicationPrescriptionWrapper =
                new RepositoryWrapper<IMedicationPrescriptionRepository>(medicationPrescriptionRepository);
        }

        public IEnumerable<MedicationPrescription> SimpleSearch(string nameQuery)
            => _medicationPrescriptionWrapper.Repository.GetMatching(
                prescription => prescription.Medication.Name.Contains(nameQuery));

        public IEnumerable<MedicationPrescription> AdvancedSearch(PrescriptionAdvancedFilterDto filterDto)
            => _medicationPrescriptionWrapper.Repository.GetMatching(filterDto.GetFilterExpression());

        public IEnumerable<MedicationPrescription> GetAll()
            => _medicationPrescriptionWrapper.Repository.GetAll();
    }
}