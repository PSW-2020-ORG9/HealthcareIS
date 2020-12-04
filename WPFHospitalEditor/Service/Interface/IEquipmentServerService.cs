using HealthcareBase.Dto;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IEquipmentServerService
    {
        IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId);

        IEnumerable<EquipmentDto> getEquipmentByType(string equipmentType);
    }
}
