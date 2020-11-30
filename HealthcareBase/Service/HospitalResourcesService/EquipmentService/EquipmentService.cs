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

namespace HealthcareBase.Service.HospitalResourcesService.EquipmentService
{
    public class EquipmentService
    {
        private readonly RepositoryWrapper<IEquipmentUnitRepository> equipmentUnitRepository;
        private readonly RepositoryWrapper<IEquipmentTypeRepository> equipmentTypeRepository;

        public EquipmentService(IEquipmentUnitRepository equipmentUnitRepository)
        {
            this.equipmentUnitRepository = new RepositoryWrapper<IEquipmentUnitRepository>(equipmentUnitRepository);
            this.equipmentTypeRepository = new RepositoryWrapper<IEquipmentTypeRepository>(equipmentTypeRepository);
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

        private void DeleteFromHospitalizations(EquipmentUnit equipmentUnit)
        {
        }

        public void DeleteByType(EquipmentType equipmentType)
        {
           
        }

        public IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId)
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
            ); ;
        }

        public Dictionary<string,EquipmentDto> GetEquipmentWithQuantityByRoomId(int roomId)
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
            return allEquipment;
        }
    }
}