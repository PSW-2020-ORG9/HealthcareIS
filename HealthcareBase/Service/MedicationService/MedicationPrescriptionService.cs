// File:    MedicationPrescriptionService.cs
// Author:  Lana
// Created: 27 May 2020 19:21:11
// Purpose: Definition of Class MedicationPrescriptionService

using System.Collections.Generic;
using HealthcareBase.Model.Medication;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MedicationRepository.Interface;

namespace HealthcareBase.Service.MedicationService
{
    public class MedicationPrescriptionService
    {
        private readonly RepositoryWrapper<IMedicationPrescriptionRepository> _medicationPrescriptionWrapper;

        public MedicationPrescriptionService(
            IMedicationPrescriptionRepository medicationPrescriptionRepository
        )
        {
            this._medicationPrescriptionWrapper =
                new RepositoryWrapper<IMedicationPrescriptionRepository>(medicationPrescriptionRepository);
        }

        public IEnumerable<MedicationPrescription> GetByName(string nameQuery)
            => _medicationPrescriptionWrapper.Repository.GetMatching(
                prescription => prescription.Medication.Name.Contains(nameQuery));

        public IEnumerable<MedicationPrescription> GetAll()
            => _medicationPrescriptionWrapper.Repository.GetAll();
    }
}