using System.Collections.Generic;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IRoomServerController
    {
        IEnumerable<Room> getRoomsByEquipmentType(string equipmentType);
    }
}
