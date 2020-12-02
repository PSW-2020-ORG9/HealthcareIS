using HealthcareBase.Dto;
using System.Collections.Generic;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor.Controller
{
    public class MedicationServerController
    {
        public MedicationServerService medicationServerService = new MedicationServerService();

        public IEnumerable<MedicationDto> getAllMedication()
        {
            return medicationServerService.getAllMedication();
        }
    }
}
