using HealthcareBase.Dto;
using System.Collections.Generic;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IEquipmentServerController
    {
        IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId);
        IEnumerable<EquipmentDto> GetEquipmentByType(string equipmentType);
        bool RealocateEquipment(EquipmentRealocationDto eqRealDto);
    }
}
