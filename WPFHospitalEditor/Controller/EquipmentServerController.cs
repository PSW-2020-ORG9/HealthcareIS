using HealthcareBase.Dto;
using System.Collections.Generic;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
   public class EquipmentServerController : IEquipmentServerController
    {
        private readonly IEquipmentServerService EquipmentServerService = new EquipmentServerService();

        public IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId)
        {
            return EquipmentServerService.GetEquipmentByRoomId(roomId);
        }

        public IEnumerable<EquipmentDto> GetEquipmentByType(string equipmentType)
        {
            return EquipmentServerService.GetEquipmentByType(equipmentType);
        }

        public bool RealocateEquipment(EquipmentRealocationDto eqRealDto)
        {
            return EquipmentServerService.RealocateEquipment(eqRealDto);
        }

    }
}
