using DesktopDTO;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IEquipmentServerService
    {
        IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId);

        IEnumerable<EquipmentDto> GetEquipmentByType(string equipmentType);
    }
}
