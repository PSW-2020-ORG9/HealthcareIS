using System.Collections.Generic;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IRoomServerService
    {
        IEnumerable<Room> GetRoomsByEquipmentType(string equipmentType);
        IEnumerable<int> GetUnavailableRooms(SchedulingDto schedulingDto);
        bool CreateRoom(CreateRoomDto createRoomDto);
        public IEnumerable<Room> GetAllRooms();

    }
}
