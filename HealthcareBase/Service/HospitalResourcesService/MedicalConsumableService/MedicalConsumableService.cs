// File:    MedicalConsumableService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:58:00
// Purpose: Definition of Class MedicalConsumableService

using Model.CustomExceptions;
using Model.HospitalResources;
using Model.StorageRecords;
using Repository.HospitalResourcesRepository;
using System;
using System.Collections.Generic;

namespace Service.HospitalResourcesService.MedicalConsumableService
{
    public class MedicalConsumableService
    {
        private MedicalConsumableRepository medicalConsumableRepository;
        private MedicalConsumableTypeRepository medicalConsumableTypeRepository;
        private ConsumableStorageRecordRepository consumableStorageRecordRepository;

        public MedicalConsumableService(MedicalConsumableRepository medicalConsumableRepository,
            MedicalConsumableTypeRepository medicalConsumableTypeRepository,
            ConsumableStorageRecordRepository consumableStorageRecordRepository)
        {
            this.medicalConsumableRepository = medicalConsumableRepository;
            this.medicalConsumableTypeRepository = medicalConsumableTypeRepository;
            this.consumableStorageRecordRepository = consumableStorageRecordRepository;
        }

        public MedicalConsumable GetByID(int id)
        {
            return medicalConsumableRepository.GetByID(id);
        }

        public IEnumerable<MedicalConsumable> GetAll()
        {
            return medicalConsumableRepository.GetAll();
        }

        public MedicalConsumable Create(MedicalConsumable medicalConsumable)
        {
            bool typeExists = medicalConsumableTypeRepository.ExistsByID(medicalConsumable.ConsumableType.GetKey());
            if (!typeExists)
                medicalConsumable.ConsumableType = medicalConsumableTypeRepository.Create(medicalConsumable.ConsumableType);
            consumableStorageRecordRepository.Create(new ConsumableStorageRecord()
            {
                AvailableAmount = 0,
                Consumable = medicalConsumable,
            }
            );
            return medicalConsumableRepository.Create(medicalConsumable);
        }

        public MedicalConsumable Update(MedicalConsumable medicalConsumable)
        {
            return medicalConsumableRepository.Update(medicalConsumable);
        }

        public void Delete(MedicalConsumable medicalConsumable)
        {
            medicalConsumableRepository.Delete(medicalConsumable);
            ConsumableStorageRecord record = consumableStorageRecordRepository.GetByMedicalConsumable(medicalConsumable);
            consumableStorageRecordRepository.Delete(record);
        }

    }
}