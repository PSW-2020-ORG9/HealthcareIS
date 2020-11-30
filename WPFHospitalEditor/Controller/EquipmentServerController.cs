﻿using HealthcareBase.Dto;
using System.Collections.Generic;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor.Controller
{
   public class EquipmentServerController
    {
        public EquipmentServerService EquipmentServerService = new EquipmentServerService();

        public IEnumerable<EquipmentDto> getEquipmentByRoomId(int roomId)
        {
            return EquipmentServerService.getEquipmentByRoomId(roomId);
        }
    }
}
