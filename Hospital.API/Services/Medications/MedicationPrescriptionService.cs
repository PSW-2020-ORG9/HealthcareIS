using General;
using General.Repository;
using Hospital.API.DTOs.Filters;
using Hospital.API.Infrastructure.Repositories.Medications;
using Hospital.API.Model.Medication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.API.Services.Medications
{
    public class MedicationPrescriptionService : IMedicationPrescriptionService
    {
        private readonly RepositoryWrapper<IMedicationPrescriptionRepository> _medicationPrescriptionWrapper;
        private readonly IConnection _diagnosisConnection;

        public MedicationPrescriptionService(
            IMedicationPrescriptionRepository medicationPrescriptionRepository,
            IConnection diagnosisConnection
        )
        {
            _medicationPrescriptionWrapper =
                new RepositoryWrapper<IMedicationPrescriptionRepository>(medicationPrescriptionRepository);
            _diagnosisConnection = diagnosisConnection;
        }

        public IEnumerable<MedicationPrescription> SimpleSearch(string nameQuery)
            => _medicationPrescriptionWrapper.Repository.GetMatching(
                prescription => prescription.Medication.Name.Contains(nameQuery));

        public IEnumerable<MedicationPrescription> AdvancedSearch(PrescriptionAdvancedFilterDto filterDto)
        {
            var prescriptions = _medicationPrescriptionWrapper.Repository.GetMatching(filterDto.GetFilterExpression());
            if (!string.IsNullOrEmpty(filterDto.Diagnosis))
                prescriptions = FilterByDiagnosis(prescriptions, filterDto.Diagnosis);
            return prescriptions;
        }

        private IEnumerable<MedicationPrescription> FilterByDiagnosis(IEnumerable<MedicationPrescription> prescriptions, string diagnosisName)
        {
            var diagnosisIds = prescriptions.Select(p => p.DiagnosisId);
            var diagnoses = FetchDiagnoses(diagnosisIds)
                .Where(d => d.Name.Equals(diagnosisName))
                .Select(d => d.Id);
            return prescriptions.Where(p => diagnoses.Contains(p.DiagnosisId));
        }

        private IEnumerable<Diagnosis> FetchDiagnoses(IEnumerable<int> diagnosisIds) 
            => _diagnosisConnection.Post<IEnumerable<Diagnosis>>(diagnosisIds);

        public IEnumerable<MedicationPrescription> GetAll()
            => _medicationPrescriptionWrapper.Repository.GetAll();
    }
}