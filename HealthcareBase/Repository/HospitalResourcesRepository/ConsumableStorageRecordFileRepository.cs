// File:    ConsumableStorageRecordFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class ConsumableStorageRecordFileRepository

using Model.CustomExceptions;
using Model.HospitalResources;
using Model.StorageRecords;
using Model.Utilities;
using Repository.Generics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.HospitalResourcesRepository
{
    public class ConsumableStorageRecordFileRepository : GenericFileRepository<ConsumableStorageRecord, int>, ConsumableStorageRecordRepository
    {
        private IntegerKeyGenerator keyGenerator;
        MedicalConsumableRepository medicalConsumableRepository;

        public ConsumableStorageRecordFileRepository(String filePath, MedicalConsumableRepository medicalConsumableRepository) : base(filePath)
        {
            this.medicalConsumableRepository = medicalConsumableRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public ConsumableStorageRecord GetByMedicalConsumable(MedicalConsumable medicalConsumable)
        {
            List<ConsumableStorageRecord> records = GetMatching(record => record.Consumable.Equals(medicalConsumable)).ToList();
            if (records.Count == 0)
                throw new BadRequestException();
            return records[0];
        }

        protected override int GenerateKey(ConsumableStorageRecord entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override ConsumableStorageRecord ParseEntity(ConsumableStorageRecord entity)
        {
            try
            {
                if (entity.Consumable != null)
                    entity.Consumable = medicalConsumableRepository.GetByID(entity.Consumable.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }
    }
}