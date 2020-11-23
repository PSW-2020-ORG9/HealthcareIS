// File:    MedicationPrescriptionService.cs
// Author:  Lana
// Created: 27 May 2020 19:21:11
// Purpose: Definition of Class MedicationPrescriptionService

using System;
using System.Collections.Generic;
using Model.CustomExceptions;
using Model.Medication;
using Model.Users.Patient;
using Repository.Generics;
using Repository.MedicationRepository;

namespace Service.MedicationService
{
    public class MedicationPrescriptionService
    {
        private readonly RepositoryWrapper<MedicationPrescriptionRepository> medicationPrescriptionRepository;

        public MedicationPrescriptionService(
            MedicationPrescriptionRepository medicationPrescriptionRepository
        )
        {
            this.medicationPrescriptionRepository =
                new RepositoryWrapper<MedicationPrescriptionRepository>(medicationPrescriptionRepository);
        }

        public IEnumerable<MedicationPrescription> GetByMedicalRecordId(int medicalRecordId)
        {
            return medicationPrescriptionRepository.Repository.GetByMedicalRecordId(medicalRecordId);
        }

        /// <summary>
        /// Create new <see cref="MedicationPrescription"/>.
        /// </summary>
        /// <param name="medicationPrescription"></param>
        /// <returns>Newly created prescription</returns>
        /// <exception cref="ArgumentException"> If argument is null. </exception>
        public MedicationPrescription Create(MedicationPrescription medicationPrescription)
        {
            if (medicationPrescription == null) throw new ArgumentException();
            return medicationPrescriptionRepository.Repository.Create(medicationPrescription);
        }
    }
}