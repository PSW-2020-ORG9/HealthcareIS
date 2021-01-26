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

        public bool CreateRoom(CreateRoomDto createRoomDto)
        {
            return roomTypeServerService.CreateRoom(createRoomDto);
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return roomTypeServerService.GetAllRooms();
        }

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