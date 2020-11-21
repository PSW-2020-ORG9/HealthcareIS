// File:    MedicalConsumableFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class MedicalConsumableFileRepository

using System.Linq;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public class MedicalConsumableFileRepository : GenericFileRepository<MedicalConsumable, int>,
        MedicalConsumableRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly MedicalConsumableTypeRepository medicalConsumableTypeRepository;

        public MedicalConsumableFileRepository(MedicalConsumableTypeRepository medicalConsumableTypeRepository,
            string filePath) : base(filePath)
        {
            this.medicalConsumableTypeRepository = medicalConsumableTypeRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public bool ExistsByType(MedicalConsumableType type)
        {
            var consumables = GetAll();
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