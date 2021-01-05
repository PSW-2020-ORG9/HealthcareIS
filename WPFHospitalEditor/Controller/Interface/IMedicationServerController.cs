using System.Collections.Generic;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Controller
{
    public interface IMedicationServerController
    {
        IEnumerable<MedicationDto> GetAllMedication();
        IEnumerable<MedicationDto> GetAllMedicationByName(string name);
        IEnumerable<MedicationDto> SearchMedications(string name);
    }
}
