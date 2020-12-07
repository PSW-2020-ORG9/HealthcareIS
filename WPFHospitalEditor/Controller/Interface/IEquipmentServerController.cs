using HealthcareBase.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IEquipmentServerController
    {
        IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId);
        IEnumerable<EquipmentDto> getEquipmentByType(string equipmentType);
    }
}
