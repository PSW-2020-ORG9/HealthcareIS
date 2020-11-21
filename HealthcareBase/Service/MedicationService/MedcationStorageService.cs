// File:    MedcationStorageService.cs
// Author:  Lana
// Created: 28 May 2020 11:41:09
// Purpose: Definition of Class MedcationStorageService

using System;
using HealthcareBase.Model.Medication;
using HealthcareBase.Model.StorageRecords;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MedicationRepository;

namespace HealthcareBase.Service.MedicationService
{
    public class MedcationStorageService
    {
        private readonly RepositoryWrapper<MedicationStorageRepository> medicatonStorageRepository;

        public MedcationStorageService(MedicationStorageRepository medicatonStorageRepository)
        {
            this.medicatonStorageRepository =
                new RepositoryWrapper<MedicationStorageRepository>(medicatonStorageRepository);
        }

        public int GetCurrentAmount(Medication medication)
        {
            var record = medicatonStorageRepository.Repository.GetByMedication(medication);
            return record.AvailableAmount;
        }

        public int IncreaseAmount(Medication medication, int amount)
        {
            var record = medicatonStorageRepository.Repository.GetByMedication(medication);
            record.AvailableAmount += amount;
            var amountChangeRecord = new AmountChangeRecord {Amount = record.AvailableAmount, Date = DateTime.Now};
            record.AddSupplyHistory(amountChangeRecord);
            medicatonStorageRepository.Repository.Update(record);
            return record.AvailableAmount;
        }

        public int DecreaseAmount(Medication medication, int amount)
        {
            var record = medicatonStorageRepository.Repository.GetByMedication(medication);
            record.AvailableAmount -= amount;
            var amountChangeRecord = new AmountChangeRecord {Amount = record.AvailableAmount, Date = DateTime.Now};
            record.AddUsageHistory(amountChangeRecord);
            medicatonStorageRepository.Repository.Update(record);
            return record.AvailableAmount;
        }
    }
}