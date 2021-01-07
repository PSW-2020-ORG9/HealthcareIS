using System.Collections.Generic;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IRoomServerService
    {
        IEnumerable<Room> GetRoomsByEquipmentType(string equipmentType);
    }
}
