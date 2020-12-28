using HealthcareBase.Dto;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service
{
    public interface IMedicationServerService
    {
        IEnumerable<MedicationDto> GetAllMedication();
        IEnumerable<MedicationDto> GetAllMedicationByName(string name);
        IEnumerable<MedicationDto> SearchMedications(string name);
    }
}
