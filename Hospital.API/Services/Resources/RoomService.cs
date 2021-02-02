using General.Repository;
using Hospital.API.DTOs;
using Hospital.API.Infrastructure.Repositories.Resources;
using Hospital.API.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.API.Services.Resources
{
    public class RoomService : IRoomService
    {
        private readonly RepositoryWrapper<IRoomRepository> _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = new RepositoryWrapper<IRoomRepository>(roomRepository);
        }

        public IEnumerable<Room> GetRoomsByIds(IEnumerable<int> ids)
            => _roomRepository.Repository.GetMatching(r => ids.Contains(r.Id));

        public Room GetById(int id)
            => _roomRepository.Repository.GetByID(id);

        public IEnumerable<Room> getByEquipmentType(string equipmentTypeName)
            => _roomRepository.Repository
            .GetMatching(r => r.Equipment.Select(r => r.EquipmentType.Name).Contains(equipmentTypeName));

        public bool CreateRoom(CreateRoomDto dto)
        {
            Room room = new Room();
            room.Id = dto.id;
            room.Name = dto.name;
            room.DepartmentId = 1;
            _roomRepository.Repository.Create(room);
            return true;
        }

        public IEnumerable<Room> GetAll()
        {
            return _roomRepository.Repository.GetAll();
        }
    }
}
