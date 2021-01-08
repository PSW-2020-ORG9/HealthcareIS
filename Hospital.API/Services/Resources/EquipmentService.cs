using System.Collections.Generic;
using System;
using System.Linq;
using Hospital.API.Model.Resources;
using General.Repository;
using Hospital.API.Infrastructure.Repositories.Resources;
using Hospital.API.DTOs;
using Hospital.API.Model.Utilities;
using Hospital.API.Model.Dependencies;

namespace Hospital.API.Services.Resources
{
    public class EquipmentService : IEquipmentService
    {
        private readonly RepositoryWrapper<IEquipmentUnitRepository> _equipmentUnitRepository;
        private readonly RepositoryWrapper<IEquipmentTypeRepository> _equipmentTypeRepository;
        private readonly RepositoryWrapper<IRoomRepository> _roomRepository;

        public EquipmentService(IEquipmentUnitRepository equipmentUnitRepository,
            IEquipmentTypeRepository equipmentTypeRepository,
            IRoomRepository roomRepository)
        {
            _equipmentUnitRepository = new RepositoryWrapper<IEquipmentUnitRepository>(equipmentUnitRepository);
            _equipmentTypeRepository = new RepositoryWrapper<IEquipmentTypeRepository>(equipmentTypeRepository);
            _roomRepository = new RepositoryWrapper<IRoomRepository>(roomRepository);
        }

        public EquipmentUnit GetByID(int id)
        {
            return _equipmentUnitRepository.Repository.GetByID(id);
        }

        public IEnumerable<EquipmentUnit> GetAll()
        {
            return _equipmentUnitRepository.Repository.GetAll();
        }

        public EquipmentUnit Create(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new ArgumentException();
            if (!_equipmentTypeRepository.Repository.ExistsByID(equipmentUnit.EquipmentType.Id))
                equipmentUnit.EquipmentType = _equipmentTypeRepository.Repository.Create(equipmentUnit.EquipmentType);
            return _equipmentUnitRepository.Repository.Create(equipmentUnit);
        }

        public EquipmentUnit Update(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new ArgumentException();
            return _equipmentUnitRepository.Repository.Update(equipmentUnit);
        }

        public void Delete(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new ArgumentException();
            _equipmentUnitRepository.Repository.Delete(equipmentUnit);
        }

        public void DeleteByType(EquipmentType equipmentType)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId)
        {
            return _equipmentUnitRepository.Repository.GetColumnsForMatching(
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
            return _equipmentUnitRepository.Repository.GetColumnsForMatching(
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

        private IEnumerable<EquipmentUnit> GetEquipmentByRoomIdAndType(int roomId, string equipmentType)
        {
            return _equipmentUnitRepository.Repository.GetMatching(
                condition: equipment => equipment.CurrentLocationId == roomId && equipment.EquipmentType.Name == equipmentType
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

        public bool RelocateEquipment(EquipmentRelocationDto eqRealDto)
        {
            List<EquipmentUnit> equipmentsInRoom = GetEquipmentByRoomIdAndType(eqRealDto.SourceRoomId, eqRealDto.EquipmentType).ToList();
            if (CheckAmount(eqRealDto.Amount, eqRealDto.SourceRoomId, eqRealDto.EquipmentType))
            {
                MoveEquipmentUnits(eqRealDto, equipmentsInRoom);
                return true;
            }
            return false;
        }

        private void MoveEquipmentUnits(EquipmentRelocationDto eqRealDto, List<EquipmentUnit> equipmentsInRoom)
        {
            for (int i = 0; i < eqRealDto.Amount; i++)
            {
                equipmentsInRoom[i].CurrentLocation = null;
                equipmentsInRoom[i].CurrentLocationId = eqRealDto.DestinationRoomId;
                _equipmentUnitRepository.Repository.Update(equipmentsInRoom[i]);
            }
        }

        private bool CheckAmount(int amount, int sourceRoomId, string equipmentType)
        {
            foreach(EquipmentDto eqDto in GetEquipmentWithQuantityByRoomId(sourceRoomId))
            {
                if(eqDto.Name.Equals(equipmentType))
                {
                    if (eqDto.Quantity >= amount) return true;
                    return false;
                }
            }
            return false;
        }
    }
}