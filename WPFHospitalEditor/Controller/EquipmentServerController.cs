using HealthcareBase.Dto;
using System.Collections.Generic;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
   public class EquipmentServerController : IEquipmentServerController
    {
        private IEquipmentServerService EquipmentServerService = new EquipmentServerService();

        public IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId)
        {
            return EquipmentServerService.GetEquipmentByRoomId(roomId);
        }

        public IEnumerable<EquipmentDto> getEquipmentsByType(string equipmentType)
        {
            return EquipmentServerService.getEquipmentsByType(equipmentType);
        }

    }
}
