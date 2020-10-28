// File:    MedicationPrescriptionService.cs
// Author:  Lana
// Created: 27 May 2020 19:21:11
// Purpose: Definition of Class MedicationPrescriptionService

using Model.Medication;
using System;
using System.Collections.Generic;
using Repository.MedicationRepository;
using Model.CustomExceptions;
using Model.Users.Patient;
using Model.Notifications;

namespace Service.MedicationService
{
    public class MedicationPrescriptionService
    {
        private MedicationPrescriptionRepository medicationPrescriptionRepository;
        private NotificationService.NotificationService notificationService;

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

            MedicationPrescription createdMedicationPrescription = medicationPrescriptionRepository.Create(medicationPrescription);
            notificationService.Notify(createdMedicationPrescription);

            return createdMedicationPrescription;
        }

        

    }
}