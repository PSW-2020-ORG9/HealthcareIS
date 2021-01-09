using System.Collections.Generic;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IEquipmentTypeServerController
    {
        IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes();
        IEnumerable<EquipmentTypeDto> SearchEquipmentTypes(string name);
    }
}
