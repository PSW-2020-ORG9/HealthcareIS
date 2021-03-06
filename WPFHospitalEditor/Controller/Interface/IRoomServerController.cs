﻿using System.Collections.Generic;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;


namespace WPFHospitalEditor.Controller.Interface
{
    public interface IRoomServerController
    {
        IEnumerable<Room> getRoomsByEquipmentType(string equipmentType);
        IEnumerable<int> GetUnavailableRooms(SchedulingDto dto);
        Room CreateRoom(CreateRoomDto createRoomDto);
        IEnumerable<Room> GetAllRooms();
    }
}
