using System.Collections.Generic;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IEquipmentServerController
    {
        IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId);
        IEnumerable<EquipmentDto> GetEquipmentByType(string equipmentType);
        bool RelocateEquipment(EquipmentRelocationDto eqRealDto);
    }
}
