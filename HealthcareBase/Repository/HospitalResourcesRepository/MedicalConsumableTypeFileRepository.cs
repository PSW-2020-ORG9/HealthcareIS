// File:    MedicalConsumableTypeFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class MedicalConsumableTypeFileRepository

using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public class MedicalConsumableTypeFileRepository : GenericFileRepository<MedicalConsumableType, int>,
        MedicalConsumableTypeRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;

        public MedicalConsumableTypeFileRepository(string filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(MedicalConsumableType entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override MedicalConsumableType ParseEntity(MedicalConsumableType entity)
        {
            return entity;
        }
    }
}