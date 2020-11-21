// File:    EquipmentTypeFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class EquipmentTypeFileRepository

using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public class EquipmentTypeFileRepository : GenericFileRepository<EquipmentType, int>, EquipmentTypeRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;

        public EquipmentTypeFileRepository(string filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(EquipmentType entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override EquipmentType ParseEntity(EquipmentType entity)
        {
            return entity;
        }
    }
}