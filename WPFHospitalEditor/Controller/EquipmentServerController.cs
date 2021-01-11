using System.Collections.Generic;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
   public class EquipmentServerController : IEquipmentServerController
    {
        private readonly IEquipmentServerService equipmentServerService = new EquipmentServerService();

        public IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId)
        {
            return equipmentServerService.GetEquipmentByRoomId(roomId);
        }

        public IEnumerable<EquipmentDto> GetEquipmentByType(string equipmentType)
        {
            return equipmentServerService.GetEquipmentByType(equipmentType);
        }

        public bool RelocateEquipment(EquipmentRelocationDto eqRealDto)
        {
            return equipmentServerService.RelocateEquipment(eqRealDto);
        }

    }
}
