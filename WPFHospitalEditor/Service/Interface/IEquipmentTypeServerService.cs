using HealthcareBase.Dto;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IEquipmentTypeServerService
    {
        IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes();
    }
}
