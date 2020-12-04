using HealthcareBase.Dto;
using System.Collections.Generic;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class EquipmentTypeServerController
    {
        private IEquipmentTypeServerService EquipmentTypeServerService = new EquipmentTypeServerService();

        public IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes()
        {
            return EquipmentTypeServerService.GetAllEquipmentTypes();
        }
    }
}
