// File:    MedicationPrescriptionService.cs
// Author:  Lana
// Created: 27 May 2020 19:21:11
// Purpose: Definition of Class MedicationPrescriptionService

using System.Collections.Generic;
using Model.CustomExceptions;
using Model.Medication;
using Model.Users.Patient;
using Repository.MedicationRepository;

namespace Service.MedicationService
{
    public class MedicationPrescriptionService
    {
        private readonly MedicationPrescriptionRepository medicationPrescriptionRepository;
        private readonly NotificationService.NotificationService notificationService;

        public MedicationPrescriptionService(MedicationPrescriptionRepository medicationPrescriptionRepository,
            NotificationService.NotificationService notificationService)
        {
            this.medicationPrescriptionRepository = medicationPrescriptionRepository;
            this.notificationService = notificationService;
        }

        public MedicationPrescription GetByID(int id)
        {
            return medicationPrescriptionRepository.GetByID(id);
        }

        public IEnumerable<MedicationPrescription> GetAll()
        {
            return medicationPrescriptionRepository.GetAll();
        }

        public IEnumerable<MedicationPrescription> GetByPatient(Patient patient)
        {
            return medicationPrescriptionRepository.GetByPatient(patient);
        }

        public MedicationPrescription Create(MedicationPrescription medicationPrescription)
        {
            if (medicationPrescription is null)
                throw new BadRequestException();

            var createdMedicationPrescription = medicationPrescriptionRepository.Create(medicationPrescription);
            notificationService.Notify(createdMedicationPrescription);

            return createdMedicationPrescription;
        }
    }
}