using DesktopDTO;
using System.Collections.Generic;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor.Controller
{
    public class MedicationServerController : IMedicationServerController
    {
        private readonly IMedicationServerService medicationServerService = new MedicationServerService();

        public IEnumerable<MedicationDto> GetAllMedication()
        {
            return medicationServerService.GetAllMedication();
        }
        public IEnumerable<MedicationDto> GetAllMedicationByName(string name)
        {
            return medicationServerService.GetAllMedicationByName(name);
        }

        public IEnumerable<MedicationDto> SearchMedications(string name)
        {
            return medicationServerService.SearchMedications(name);
        }
    }
}
