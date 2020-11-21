// File:    MedicalConsumableStorageService.cs
// Author:  Lana
// Created: 28 May 2020 11:43:39
// Purpose: Definition of Class MedicalConsumableStorageService

using System;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.StorageRecords;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;

namespace HealthcareBase.Service.HospitalResourcesService.MedicalConsumableService
{
    public class MedicalConsumableStorageService
    {
        private readonly RepositoryWrapper<ConsumableStorageRecordRepository> consumableStorageRecordRepository;

        public MedicalConsumableStorageService(
            ConsumableStorageRecordRepository consumableStorageRecordRepository)
        {
            this.consumableStorageRecordRepository =
                new RepositoryWrapper<ConsumableStorageRecordRepository>(consumableStorageRecordRepository);
        }

        public int GetCurrentAmount(MedicalConsumable consumable)
        {
            var record = consumableStorageRecordRepository.Repository.GetByMedicalConsumable(consumable);
            return record.AvailableAmount;
        }

        public int IncreaseAmount(MedicalConsumable consumable, int amount)
        {
            var record = consumableStorageRecordRepository.Repository.GetByMedicalConsumable(consumable);
            record.AvailableAmount += amount;
            var amountChangeRecord = new AmountChangeRecord {Amount = record.AvailableAmount, Date = DateTime.Now};
            record.AddSupplyHistory(amountChangeRecord);
            consumableStorageRecordRepository.Repository.Update(record);
            return record.AvailableAmount;
        }

        public int DecreaseAmount(MedicalConsumable consumable, int amount)
        {
            var record = consumableStorageRecordRepository.Repository.GetByMedicalConsumable(consumable);
            record.AvailableAmount -= amount;
            var amountChangeRecord = new AmountChangeRecord {Amount = record.AvailableAmount, Date = DateTime.Now};
            record.AddUsageHistory(amountChangeRecord);
            consumableStorageRecordRepository.Repository.Update(record);
            return record.AvailableAmount;
        }
    }
}