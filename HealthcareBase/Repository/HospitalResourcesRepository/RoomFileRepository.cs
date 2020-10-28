// File:    RoomFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class RoomFileRepository

using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Utilities;
using Repository.Generics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.HospitalResourcesRepository
{
    public class RoomFileRepository : GenericFileRepository<Room, int>, RoomRepository
    {
        private IntegerKeyGenerator keyGenerator;
        private EquipmentUnitRepository equipmentUnitRepository;
        private DepartmentRepository departmentRepository;

        public EquipmentUnitRepository EquipmentUnitRepository { get => equipmentUnitRepository; set => equipmentUnitRepository = value; }

        public RoomFileRepository(DepartmentRepository departmentRepository, String filePath) : base(filePath)
        {
            this.departmentRepository = departmentRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<Room> GetByEquipment(IEnumerable<EquipmentType> equipmentTypes)
        {
            List<Room> roomsWithCorrectEquipment = GetAll().ToList();
            foreach (Room room in GetAll())
            {
                foreach (EquipmentType equipmentType in equipmentTypes)
                    if (!room.Equipment.Any(equipmentUnit => equipmentType.Equals(equipmentUnit.EquipmentType)))
                    {
                        roomsWithCorrectEquipment.Remove(room);
                        break;
                    }
            }
            return roomsWithCorrectEquipment;
        }

        public IEnumerable<Room> GetByDepartment(Department department)
        {
            return GetMatching(room => department.Equals(room.Department));
        }

        public override Room Update(Room entity)
        {
            entity.RemoveAllEquipment();
            return base.Update(entity);
        }

        public override Room Create(Room entity)
        {
            entity.RemoveAllEquipment();
            return base.Create(entity);
        }
        
        protected override Room ParseEntity(Room entity)
        {
            try
            {
                entity.Equipment = equipmentUnitRepository.GetByCurrentLocationWithoutParse(entity);
                foreach (EquipmentUnit equipment in entity.Equipment)
                    equipment.CurrentLocation = entity;
                if (entity.Department != null)
                    entity.Department = departmentRepository.GetByID(entity.Department.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }

        protected override int GenerateKey(Room entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}