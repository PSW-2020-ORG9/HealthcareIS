// File:    MedicationStorageFileRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:05:05
// Purpose: Definition of Class MedicationStorageFileRepository

using Model.CustomExceptions;
using Model.Medication;
using Model.StorageRecords;
using Model.Utilities;
using Repository.Generics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.MedicationRepository
{
    public class MedicationStorageFileRepository : GenericFileRepository<MedicationStorageRecord, int>, MedicationStorageRepository
    {
        private IntegerKeyGenerator keyGenerator;
        MedicationRepository medicationRepository;

        public MedicationStorageFileRepository(MedicationRepository medicationRepository, String filePath) : base(filePath)
        {
            this.medicationRepository = medicationRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public MedicationStorageRecord GetByMedication(Medication medication)
        {
            IEnumerable<MedicationStorageRecord> matching = GetMatching(record => record.Medication.Equals(medication));
            if (matching.Count() == 0)
                throw new BadRequestException();
            return matching.ToList()[0];
        }

        protected override int GenerateKey(MedicationStorageRecord entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override MedicationStorageRecord ParseEntity(MedicationStorageRecord entity)
        {
            try
            {
                if (entity.Medication != null)
                    entity.Medication = medicationRepository.GetByID(entity.Medication.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }


    }
}