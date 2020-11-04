// File:    MedicalConsumableService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:58:00
// Purpose: Definition of Class MedicalConsumableService

using System.Collections.Generic;
using Model.HospitalResources;
using Model.StorageRecords;
using Repository.Generics;
using Repository.HospitalResourcesRepository;

namespace Service.HospitalResourcesService.MedicalConsumableService
{
    public class MedicalConsumableService
    {
        private readonly RepositoryWrapper<ConsumableStorageRecordRepository> consumableStorageRecordRepository;
        private readonly RepositoryWrapper<MedicalConsumableRepository> medicalConsumableRepository;
        private readonly RepositoryWrapper<MedicalConsumableTypeRepository> medicalConsumableTypeRepository;

        public MedicalConsumableService(RepositoryWrapper<MedicalConsumableRepository> medicalConsumableRepository,
            RepositoryWrapper<MedicalConsumableTypeRepository> medicalConsumableTypeRepository,
            RepositoryWrapper<ConsumableStorageRecordRepository> consumableStorageRecordRepository)
        {
            this.medicalConsumableRepository = medicalConsumableRepository;
            this.medicalConsumableTypeRepository = medicalConsumableTypeRepository;
            this.consumableStorageRecordRepository = consumableStorageRecordRepository;
        }

        public MedicalConsumable GetByID(int id)
        {
            return medicalConsumableRepository.Repository.GetByID(id);
        }

        public IEnumerable<MedicalConsumable> GetAll()
        {
            return medicalConsumableRepository.Repository.GetAll();
        }

        public MedicalConsumable Create(MedicalConsumable medicalConsumable)
        {
            var typeExists = medicalConsumableTypeRepository.Repository.ExistsByID(medicalConsumable.ConsumableType.GetKey());
            if (!typeExists)
                medicalConsumable.ConsumableType =
                    medicalConsumableTypeRepository.Repository.Create(medicalConsumable.ConsumableType);
            consumableStorageRecordRepository.Repository.Create(new ConsumableStorageRecord
                {
                    AvailableAmount = 0,
                    Consumable = medicalConsumable
                }
            );
            return medicalConsumableRepository.Repository.Create(medicalConsumable);
        }

        public MedicalConsumable Update(MedicalConsumable medicalConsumable)
        {
            return medicalConsumableRepository.Repository.Update(medicalConsumable);
        }

        public void Delete(MedicalConsumable medicalConsumable)
        {
            medicalConsumableRepository.Repository.Delete(medicalConsumable);
            var record = consumableStorageRecordRepository.Repository.GetByMedicalConsumable(medicalConsumable);
            consumableStorageRecordRepository.Repository.Delete(record);
        }
    }
}