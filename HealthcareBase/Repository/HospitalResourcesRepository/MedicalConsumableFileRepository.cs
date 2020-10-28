// File:    MedicalConsumableFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class MedicalConsumableFileRepository

using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Utilities;
using Repository.Generics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Repository.HospitalResourcesRepository
{
    public class MedicalConsumableFileRepository : GenericFileRepository<MedicalConsumable, int>, MedicalConsumableRepository
    {
        private MedicalConsumableTypeRepository medicalConsumableTypeRepository;
        private IntegerKeyGenerator keyGenerator;

        public MedicalConsumableFileRepository(MedicalConsumableTypeRepository medicalConsumableTypeRepository, String filePath) : base(filePath)
        {
            this.medicalConsumableTypeRepository = medicalConsumableTypeRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public bool ExistsByType(MedicalConsumableType type)
        {
            IEnumerable<MedicalConsumable> consumables = GetAll();
            if (consumables.Any(consumable => consumable.ConsumableType.Equals(type)))
                return true;
            return false;
        }

        protected override int GenerateKey(MedicalConsumable entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override MedicalConsumable ParseEntity(MedicalConsumable entity)
        {
            try
            {
                if (entity.ConsumableType != null)
                    entity.ConsumableType = medicalConsumableTypeRepository.GetByID(entity.ConsumableType.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }
    }
}