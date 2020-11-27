using HealthcareBase.Model.HospitalResources;
using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor.Controller
{
   public class EquipmentServerController
    {
        public EquipmentServerService EquipmentServerService = new EquipmentServerService();

        public IEnumerable<EquipmentUnit> getEquipmentByRoomId(int roomId)
        {
            return EquipmentServerService.getEquipmentByRoomId(roomId);
        }
    }
}
