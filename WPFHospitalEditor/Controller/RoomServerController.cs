using System.Collections.Generic;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class RoomServerController : IRoomServerController
    {
        private readonly IRoomServerService roomTypeServerService = new RoomServerService();

        public IEnumerable<Room> getRoomsByEquipmentType(string equipmentType)
        {
            return roomTypeServerService.GetRoomsByEquipmentType(equipmentType);
        }

        public IEnumerable<int> GetUnavailableRooms(SchedulingDto dto)
        {
            return roomTypeServerService.GetUnavailableRooms(dto);
        }
    }
}