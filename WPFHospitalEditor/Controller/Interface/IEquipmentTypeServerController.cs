using HealthcareBase.Dto;
using System.Collections.Generic;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IEquipmentTypeServerController
    {
        IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes();
    }
}
