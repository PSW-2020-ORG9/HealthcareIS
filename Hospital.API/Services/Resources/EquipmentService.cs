using System.Collections.Generic;
using System;
using System.Linq;
using Hospital.API.Model.Resources;
using General.Repository;
using Hospital.API.Infrastructure.Repositories.Resources;
using Hospital.API.DTOs;

namespace Hospital.API.Services.Resources
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
                throw new ArgumentException();
            if (!equipmentTypeRepository.Repository.ExistsByID(equipmentUnit.EquipmentType.Id))
                equipmentUnit.EquipmentType = equipmentTypeRepository.Repository.Create(equipmentUnit.EquipmentType);
            return equipmentUnitRepository.Repository.Create(equipmentUnit);
        }

        public EquipmentUnit Update(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new ArgumentException();
            return equipmentUnitRepository.Repository.Update(equipmentUnit);
        }

        public void Delete(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new ArgumentException();
            equipmentUnitRepository.Repository.Delete(equipmentUnit);
        }

        public void DeleteByType(EquipmentType equipmentType)
        {
            throw new NotImplementedException();
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

        private IEnumerable<EquipmentDto> GetEquipmentByType(string equipmentType)
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
            foreach (EquipmentDto equipment in GetEquipmentByType(equipmentType))
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