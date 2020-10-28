// File:    MedicalConsumableStorageService.cs
// Author:  Lana
// Created: 28 May 2020 11:43:39
// Purpose: Definition of Class MedicalConsumableStorageService

using Model.HospitalResources;
using Model.StorageRecords;
using Repository.HospitalResourcesRepository;
using System;

namespace Service.HospitalResourcesService.MedicalConsumableService
{
    public class MedicalConsumableStorageService
    {
        private ConsumableStorageRecordRepository consumableStorageRecordRepository;

        public MedicalConsumableStorageService(ConsumableStorageRecordRepository consumableStorageRecordRepository)
        {
            this.consumableStorageRecordRepository = consumableStorageRecordRepository;
        }

        public int GetCurrentAmount(MedicalConsumable consumable)
        {
            ConsumableStorageRecord record = consumableStorageRecordRepository.GetByMedicalConsumable(consumable);
            return record.AvailableAmount;
        }

        public int IncreaseAmount(MedicalConsumable consumable, int amount)
        {
            ConsumableStorageRecord record = consumableStorageRecordRepository.GetByMedicalConsumable(consumable);
            record.AvailableAmount += amount;
            AmountChangeRecord amountChangeRecord = new AmountChangeRecord() { Amount = record.AvailableAmount, Date = DateTime.Now };
            record.AddSupplyHistory(amountChangeRecord);
            consumableStorageRecordRepository.Update(record);
            return record.AvailableAmount;
        }

        public int DecreaseAmount(MedicalConsumable consumable, int amount)
        {
            ConsumableStorageRecord record = consumableStorageRecordRepository.GetByMedicalConsumable(consumable);
            record.AvailableAmount -= amount;
            AmountChangeRecord amountChangeRecord = new AmountChangeRecord() { Amount = record.AvailableAmount, Date = DateTime.Now };
            record.AddUsageHistory(amountChangeRecord);
            consumableStorageRecordRepository.Update(record);
            return record.AvailableAmount;
        }

    }
}