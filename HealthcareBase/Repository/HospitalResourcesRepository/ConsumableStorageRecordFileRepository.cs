// File:    ConsumableStorageRecordFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class ConsumableStorageRecordFileRepository

using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Model.StorageRecords;
using Model.Utilities;
using Repository.Generics;

namespace Repository.HospitalResourcesRepository
{
    public class ConsumableStorageRecordFileRepository : GenericFileRepository<ConsumableStorageRecord, int>,
        ConsumableStorageRecordRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly MedicalConsumableRepository medicalConsumableRepository;

        public ConsumableStorageRecordFileRepository(string filePath,
            MedicalConsumableRepository medicalConsumableRepository) : base(filePath)
        {
            this.medicalConsumableRepository = medicalConsumableRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public ConsumableStorageRecord GetByMedicalConsumable(MedicalConsumable medicalConsumable)
        {
            var records = GetMatching(record => record.Consumable.Equals(medicalConsumable)).ToList();
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