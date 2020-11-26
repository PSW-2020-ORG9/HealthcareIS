// File:    MedicalConsumableService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:58:00
// Purpose: Definition of Class MedicalConsumableService

using System.Collections.Generic;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.StorageRecords;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;

namespace HealthcareBase.Service.HospitalResourcesService.MedicalConsumableService
{
    public class MedicalConsumableService
    {
        private readonly RepositoryWrapper<IConsumableStorageRecordRepository> consumableStorageRecordRepository;
        private readonly RepositoryWrapper<IMedicalConsumableRepository> medicalConsumableRepository;
        private readonly RepositoryWrapper<IMedicalConsumableTypeRepository> medicalConsumableTypeRepository;

        public MedicalConsumableService(
            IMedicalConsumableRepository medicalConsumableRepository,
            IMedicalConsumableTypeRepository medicalConsumableTypeRepository,
            IConsumableStorageRecordRepository consumableStorageRecordRepository)
        {
            this.medicalConsumableRepository =
                new RepositoryWrapper<IMedicalConsumableRepository>(medicalConsumableRepository);
            this.medicalConsumableTypeRepository =
                new RepositoryWrapper<IMedicalConsumableTypeRepository>(medicalConsumableTypeRepository);
            this.consumableStorageRecordRepository =
                new RepositoryWrapper<IConsumableStorageRecordRepository>(consumableStorageRecordRepository);
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
            var typeExists =
                medicalConsumableTypeRepository.Repository.ExistsByID(medicalConsumable.ConsumableType.GetKey());
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