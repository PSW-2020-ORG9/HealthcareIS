using DesktopDTO;
using System.Collections.Generic;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IEquipmentTypeServerController
    {
        IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes();
        IEnumerable<EquipmentTypeDto> SearchEquipmentTypes(string name);
    }
}
