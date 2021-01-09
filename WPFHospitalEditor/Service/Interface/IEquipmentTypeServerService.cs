using System.Collections.Generic;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IEquipmentTypeServerService
    {
        IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes();
        IEnumerable<EquipmentTypeDto> SearchEquipmentTypes(string name);
    }
}
