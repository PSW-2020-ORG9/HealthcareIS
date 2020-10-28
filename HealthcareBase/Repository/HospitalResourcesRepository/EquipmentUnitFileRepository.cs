// File:    EquipmentUnitFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class EquipmentUnitFileRepository

using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Utilities;
using Repository.Generics;

namespace Repository.HospitalResourcesRepository
{
    public class EquipmentUnitFileRepository : GenericFileRepository<EquipmentUnit, int>, EquipmentUnitRepository
    {
        private readonly EquipmentTypeRepository equipmentTypeRepository;
        private readonly IntegerKeyGenerator keyGenerator;

        public EquipmentUnitFileRepository(EquipmentTypeRepository equipmentTypeRepository, string filePath) :
            base(filePath)
        {
            this.equipmentTypeRepository = equipmentTypeRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public RoomRepository RoomRepository { get; set; }

        public override EquipmentUnit Create(EquipmentUnit entity)
        {
            if (entity.CurrentLocation != null)
                entity.CurrentLocation.RemoveAllEquipment();
            return base.Create(entity);
        }

        public override EquipmentUnit Update(EquipmentUnit entity)
        {
            if (entity.CurrentLocation != null)
                entity.CurrentLocation.RemoveAllEquipment();
            return base.Update(entity);
        }

        public IEnumerable<EquipmentUnit> GetByCurrentLocationWithoutParse(Room room)
        {
            var equipment = ReadFile();
            var filteredEquipment = equipment.Where(unit => room.Equals(unit.CurrentLocation));
            foreach (var entity in filteredEquipment)
                try
                {
                    if (entity.EquipmentType != null)
                        entity.EquipmentType = equipmentTypeRepository.GetByID(entity.EquipmentType.GetKey());
                }
                catch (BadRequestException)
                {
                    throw new BadReferenceException();
                }

            return filteredEquipment;
        }

        protected override EquipmentUnit ParseEntity(EquipmentUnit entity)
        {
            try
            {
                if (entity.CurrentLocation != null)
                    entity.CurrentLocation = RoomRepository.GetByID(entity.CurrentLocation.GetKey());
                if (entity.EquipmentType != null)
                    entity.EquipmentType = equipmentTypeRepository.GetByID(entity.EquipmentType.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }

        protected override int GenerateKey(EquipmentUnit entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}