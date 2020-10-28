// File:    MedicationFileRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:04:39
// Purpose: Definition of Class MedicationFileRepository

using Model.Medication;
using Model.Utilities;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.MedicationRepository
{
    public class MedicationFileRepository : GenericFileRepository<Medication, int>, MedicationRepository
    {
        private IntegerKeyGenerator keyGenerator;

        public MedicationFileRepository(String filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(Medication entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override Medication ParseEntity(Medication entity)
        {
            return entity;
        }
    }
}