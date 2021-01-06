using System.Collections.Generic;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class EquipmentTypeServerController : IEquipmentTypeServerController
    {
        private readonly IEquipmentTypeServerService EquipmentTypeServerService = new EquipmentTypeServerService();

        public IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes()
        {
            return EquipmentTypeServerService.GetAllEquipmentTypes();
        }

        public IEnumerable<EquipmentTypeDto> SearchEquipmentTypes(string name)
        {
            return EquipmentTypeServerService.SearchEquipmentTypes(name);
        }
    }
}
