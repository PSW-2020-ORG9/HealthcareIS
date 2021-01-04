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

        public MedicationPrescriptionService(IMedicationPrescriptionRepository medicationPrescriptionRepository)
        {
            _medicationPrescriptionWrapper =
                new RepositoryWrapper<IMedicationPrescriptionRepository>(medicationPrescriptionRepository);
        }

        public IEnumerable<MedicationPrescription> SimpleSearch(string nameQuery)
        {
            var prescriptions = _medicationPrescriptionWrapper.Repository.GetMatching(
                prescription => prescription.Medication.Name.Contains(nameQuery));
            return AttachDiagnoses(prescriptions);
        }

        public IEnumerable<MedicationPrescription> AdvancedSearch(PrescriptionAdvancedFilterDto filterDto)
        {
            var prescriptions = _medicationPrescriptionWrapper.Repository.GetMatching(filterDto.GetFilterExpression());
            if (!string.IsNullOrEmpty(filterDto.Diagnosis))
                prescriptions = FilterByDiagnosis(prescriptions, filterDto.Diagnosis);
            return AttachDiagnoses(prescriptions);
        }

        private IEnumerable<MedicationPrescription> FilterByDiagnosis(IEnumerable<MedicationPrescription> prescriptions, string diagnosisName)
        {
            var diagnosisIds = prescriptions.Select(p => p.DiagnosisId);
            var diagnoses = FetchDiagnoses(diagnosisIds)
                .Where(d => d.Name.Equals(diagnosisName))
                .Select(d => d.Id);
            return prescriptions.Where(p => diagnoses.Contains(p.DiagnosisId));
        }

        private IEnumerable<MedicationPrescription> AttachDiagnoses(IEnumerable<MedicationPrescription> prescriptions)
        {
            List<int> diagnosisIds = new List<int>();
            foreach (var prescription in prescriptions)
            {
                diagnosisIds.Add(prescription.DiagnosisId);
            }
            IEnumerable<Diagnosis> diagnoses = FetchDiagnoses(diagnosisIds);
            foreach (var prescription in prescriptions)
            {
                prescription.Diagnosis = diagnoses.Where(d => d.Id == prescription.DiagnosisId).FirstOrDefault();
            }
            return prescriptions;
        }

        private IEnumerable<Diagnosis> FetchDiagnoses(IEnumerable<int> diagnosisIds) 
            => _diagnosisConnection.Post<IEnumerable<Diagnosis>>(diagnosisIds);

        public IEnumerable<MedicationPrescription> GetAll()
            => AttachDiagnoses(_medicationPrescriptionWrapper.Repository.GetAll());
    }
}