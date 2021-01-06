using System.Collections.Generic;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Service
{
    public interface IMedicationServerService
    {
        IEnumerable<MedicationDto> GetAllMedication();
        IEnumerable<MedicationDto> GetAllMedicationByName(string name);
        IEnumerable<MedicationDto> SearchMedications(string name);
    }
}
