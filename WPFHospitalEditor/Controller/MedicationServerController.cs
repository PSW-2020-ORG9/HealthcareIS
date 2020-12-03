using HealthcareBase.Dto;
using System.Collections.Generic;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor.Controller
{
    public class MedicationServerController : IMedicationServerController
    {
        private IMedicationServerService medicationServerService = new MedicationServerService();

        public IEnumerable<MedicationDto> GetAllMedication()
        {
            return medicationServerService.GetAllMedication();
        }
    }
}
