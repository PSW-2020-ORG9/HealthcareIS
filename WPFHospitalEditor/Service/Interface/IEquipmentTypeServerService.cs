using DesktopDTO;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IEquipmentTypeServerService
    {
        IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes();
        IEnumerable<EquipmentTypeDto> SearchEquipmentTypes(string name);
    }
}
