// File:    MedicationPrescriptionService.cs
// Author:  Lana
// Created: 27 May 2020 19:21:11
// Purpose: Definition of Class MedicationPrescriptionService

using System;
using System.Collections.Generic;
using HealthcareBase.Model.Filters;
using Model.CustomExceptions;
using Model.Medication;
using Model.Users.Patient;
using Repository.Generics;
using Repository.MedicationRepository;

namespace Service.MedicationService
{
    public class MedicationPrescriptionService
    {
        private readonly RepositoryWrapper<MedicationPrescriptionRepository> _medicationPrescriptionWrapper;

        public MedicationPrescriptionService(
            MedicationPrescriptionRepository medicationPrescriptionRepository
        )
        {
            this._medicationPrescriptionWrapper =
                new RepositoryWrapper<MedicationPrescriptionRepository>(medicationPrescriptionRepository);
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