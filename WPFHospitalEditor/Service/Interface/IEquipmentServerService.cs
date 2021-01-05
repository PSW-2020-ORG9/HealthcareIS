using System.Collections.Generic;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IEquipmentServerService
    {
        IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId);

        IEnumerable<EquipmentDto> GetEquipmentByType(string equipmentType);
    }
}
