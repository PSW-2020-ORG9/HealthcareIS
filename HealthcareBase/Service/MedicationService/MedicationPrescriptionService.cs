// File:    MedicationPrescriptionService.cs
// Author:  Lana
// Created: 27 May 2020 19:21:11
// Purpose: Definition of Class MedicationPrescriptionService

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
        private readonly NotificationService.NotificationService notificationService;

        public MedicationPrescriptionService(
            MedicationPrescriptionRepository medicationPrescriptionRepository,
            NotificationService.NotificationService notificationService
        )
        {
            this.medicationPrescriptionRepository =
                new RepositoryWrapper<MedicationPrescriptionRepository>(medicationPrescriptionRepository);
            this.notificationService = notificationService;
        }

        public MedicationPrescription GetByID(int id)
        {
            return medicationPrescriptionRepository.Repository.GetByID(id);
        }

        public IEnumerable<MedicationPrescription> GetAll()
        {
            return medicationPrescriptionRepository.Repository.GetAll();
        }

        public IEnumerable<MedicationPrescription> GetByPatient(Patient patient)
        {
            return medicationPrescriptionRepository.Repository.GetByPatient(patient);
        }

        public MedicationPrescription Create(MedicationPrescription medicationPrescription)
        {
            if (medicationPrescription is null)
                throw new BadRequestException();

            var createdMedicationPrescription =
                medicationPrescriptionRepository.Repository.Create(medicationPrescription);
            notificationService.Notify(createdMedicationPrescription);

            return createdMedicationPrescription;
        }
    }
}