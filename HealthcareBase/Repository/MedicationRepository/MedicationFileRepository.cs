// File:    MedicationFileRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:04:39
// Purpose: Definition of Class MedicationFileRepository

using HealthcareBase.Model.Medication;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.MedicationRepository
{
    public class MedicationFileRepository : GenericFileRepository<Medication, int>, MedicationRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;

        public MedicationFileRepository(string filePath) : base(filePath)
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