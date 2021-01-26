using Hospital.API.DTOs;
using Hospital.API.Model.Resources;
using System.Collections.Generic;

namespace Hospital.API.Services.Resources
{
    public interface IRoomService
    {
        IEnumerable<Room> GetRoomsByIds(IEnumerable<int> ids);
        public Room GetById(int id);
        IEnumerable<Room> getByEquipmentType(string equipmentTypeName);
        bool CreateRoom(CreateRoomDto dto);
        IEnumerable<Room> GetAll();
    }
}
