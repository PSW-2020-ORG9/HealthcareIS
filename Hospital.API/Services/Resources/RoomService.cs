using General.Repository;
using Hospital.API.Infrastructure.Repositories.Resources;
using Hospital.API.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

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
        {
            List<Room> roomsWithEquipmentType = new List<Room>();
            foreach(Room room in _roomRepository.Repository.GetAll())
            {
                foreach(EquipmentUnit equipment in room.Equipment.ToList())
                {
                    if(compareTypes(equipment.EquipmentType.Name, equipmentTypeName))
                    {
                        roomsWithEquipmentType.Add(room);
                    }
                }
            }
            return roomsWithEquipmentType;
        }

        private bool compareTypes(string nameOne, string nameTwo)
        {
            if (nameOne.Equals(nameTwo)) return true;
            return false;
        }
    }
}
