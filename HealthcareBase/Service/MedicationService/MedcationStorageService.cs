// File:    MedcationStorageService.cs
// Author:  Lana
// Created: 28 May 2020 11:41:09
// Purpose: Definition of Class MedcationStorageService

using Model.Medication;
using Model.StorageRecords;
using Repository.MedicationRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicationService
{
    public class MedcationStorageService
    {
        private MedicationStorageRepository medicatonStorageRepository;

        public MedcationStorageService(MedicationStorageRepository medicatonStorageRepository)
        {
            this.medicatonStorageRepository = medicatonStorageRepository;
        }

        public int GetCurrentAmount(Medication medication)
        {
            MedicationStorageRecord record = medicatonStorageRepository.GetByMedication(medication);
            return record.AvailableAmount;
        }

        public int IncreaseAmount(Medication medication, int amount)
        {
            MedicationStorageRecord record = medicatonStorageRepository.GetByMedication(medication);
            record.AvailableAmount += amount;
            AmountChangeRecord amountChangeRecord = new AmountChangeRecord() { Amount = record.AvailableAmount, Date = DateTime.Now };
            record.AddSupplyHistory(amountChangeRecord);
            medicatonStorageRepository.Update(record);
            return record.AvailableAmount;
        }

        public int DecreaseAmount(Medication medication, int amount)
        {
            MedicationStorageRecord record = medicatonStorageRepository.GetByMedication(medication);
            record.AvailableAmount -= amount;
            AmountChangeRecord amountChangeRecord = new AmountChangeRecord() { Amount = record.AvailableAmount, Date = DateTime.Now };
            record.AddUsageHistory(amountChangeRecord);
            medicatonStorageRepository.Update(record);
            return record.AvailableAmount;
        }

    }
}