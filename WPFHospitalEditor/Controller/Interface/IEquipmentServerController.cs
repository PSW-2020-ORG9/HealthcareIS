using HealthcareBase.Dto;
using System.Collections.Generic;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IEquipmentServerController
    {
        IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId);
        IEnumerable<EquipmentDto> getEquipmentByType(string equipmentType);
    }
}
