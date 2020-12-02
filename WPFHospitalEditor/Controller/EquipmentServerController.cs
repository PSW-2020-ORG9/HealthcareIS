using HealthcareBase.Dto;
using System.Collections.Generic;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor.Controller
{
   public class EquipmentServerController
    {
        private EquipmentServerService EquipmentServerService = new EquipmentServerService();

        public IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId)
        {
            return EquipmentServerService.GetEquipmentByRoomId(roomId);
        }
    }
}
