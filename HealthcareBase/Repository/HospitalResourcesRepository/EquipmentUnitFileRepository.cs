// File:    EquipmentUnitFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class EquipmentUnitFileRepository

using Model.CustomExceptions;
using Model.HospitalResources;
using Repository.Generics;
using System;
using System.Linq;
using System.Collections.Generic;
using Model.Utilities;

namespace Repository.HospitalResourcesRepository
{
    public class EquipmentUnitFileRepository : GenericFileRepository<EquipmentUnit, int>, EquipmentUnitRepository
    {
        private IntegerKeyGenerator keyGenerator;
        private RoomRepository roomRepository;
        private EquipmentTypeRepository equipmentTypeRepository;

        public RoomRepository RoomRepository { get => roomRepository; set => roomRepository = value; }

        public EquipmentUnitFileRepository(EquipmentTypeRepository equipmentTypeRepository, String filePath) : base(filePath)
        {
            this.equipmentTypeRepository = equipmentTypeRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

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
            List<EquipmentUnit> equipment = ReadFile();
            IEnumerable<EquipmentUnit> filteredEquipment = equipment.Where(unit => room.Equals(unit.CurrentLocation));
            foreach (EquipmentUnit entity in filteredEquipment)
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
                    entity.CurrentLocation = roomRepository.GetByID(entity.CurrentLocation.GetKey());
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