// File:    MedicalConsumableTypeFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class MedicalConsumableTypeFileRepository

using Model.HospitalResources;
using Model.Utilities;
using Repository.Generics;
using System;

namespace Repository.HospitalResourcesRepository
{
    public class MedicalConsumableTypeFileRepository : GenericFileRepository<MedicalConsumableType, int>, MedicalConsumableTypeRepository
    {
        private IntegerKeyGenerator keyGenerator;

        public MedicalConsumableTypeFileRepository(String filePath) : base(filePath)
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