// File:    EquipmentService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:57:59
// Purpose: Definition of Class EquipmentService

using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Dto;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;
using System.Linq;
using HealthcareBase.Service.HospitalResourcesService.EquipmentService.Interface;

namespace HealthcareBase.Service.HospitalResourcesService.EquipmentService
{
    public class EquipmentService : IEquipmentService
    {
        private readonly RepositoryWrapper<IEquipmentUnitRepository> equipmentUnitRepository;
        private readonly RepositoryWrapper<IEquipmentTypeRepository> equipmentTypeRepository;

        public EquipmentService(IEquipmentUnitRepository equipmentUnitRepository)
        {
            this.equipmentUnitRepository = new RepositoryWrapper<IEquipmentUnitRepository>(equipmentUnitRepository);
        }

        public EquipmentUnit GetByID(int id)
        {
            return equipmentUnitRepository.Repository.GetByID(id);
        }

        public IEnumerable<EquipmentUnit> GetAll()
        {
            return equipmentUnitRepository.Repository.GetAll();
        }

        public EquipmentUnit Create(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new BadRequestException();
            if (!equipmentTypeRepository.Repository.ExistsByID(equipmentUnit.EquipmentType.GetKey()))
                equipmentUnit.EquipmentType = equipmentTypeRepository.Repository.Create(equipmentUnit.EquipmentType);
            return equipmentUnitRepository.Repository.Create(equipmentUnit);
        }

        public EquipmentUnit Update(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new BadRequestException();
            return equipmentUnitRepository.Repository.Update(equipmentUnit);
        }

        public void Delete(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new BadRequestException();
            DeleteFromHospitalizations(equipmentUnit);
            equipmentUnitRepository.Repository.Delete(equipmentUnit);
        }

        private void DeleteFromHospitalizations(EquipmentUnit equimpentUnit)
        {
        }

        public void DeleteByType(EquipmentType equipmentType)
        {
           
        }

        private IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId)
        {
            return equipmentUnitRepository.Repository.GetColumnsForMatching(
                condition: equipment => equipment.CurrentLocationId == roomId,
                selection: equipment => new EquipmentDto()
                {
                    Id = equipment.Id,
                    RoomId = equipment.CurrentLocation.Id,
                    Name = equipment.EquipmentType.Name,
                    Quantity = 0
                }
            );
        }

        public IEnumerable<EquipmentDto> GetEquipmentWithQuantityByRoomId(int roomId)
        {
            Dictionary<string, EquipmentDto> allEquipment = new Dictionary<string, EquipmentDto>();
            foreach (EquipmentDto equipment in GetEquipmentByRoomId(roomId)) 
            {
                if (!allEquipment.ContainsKey(equipment.Name))
                {
                    allEquipment[equipment.Name] = equipment;

                }
                allEquipment[equipment.Name].Quantity += 1;
            }
            return allEquipment.Values.ToList();
        }

        private IEnumerable<EquipmentDto> GetEquipmentsByType(string equipmentType)
        {
            return equipmentUnitRepository.Repository.GetColumnsForMatching(
                condition: equipment => equipment.EquipmentType.Name.Equals(equipmentType),
                selection: equipment => new EquipmentDto()
                {
                    Id = equipment.Id,
                    RoomId = equipment.CurrentLocation.Id,
                    Name = equipment.EquipmentType.Name,
                    Quantity = 0
                }
            );
        }

        public IEnumerable<EquipmentDto> GetEquipmentWithQuantityByType(string equipmentType)
        {
            Dictionary<int, EquipmentDto> allEquipment = new Dictionary<int, EquipmentDto>();
            foreach (EquipmentDto equipment in GetEquipmentsByType(equipmentType))
            {
                if (!allEquipment.ContainsKey(equipment.RoomId))
                {
                    allEquipment[equipment.RoomId] = equipment;

                }
                allEquipment[equipment.RoomId].Quantity += 1;
            }
            return allEquipment.Values.ToList();
        }

    }
}