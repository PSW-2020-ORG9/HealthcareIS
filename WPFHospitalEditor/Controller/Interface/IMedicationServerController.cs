using HealthcareBase.Dto;
using System.Collections.Generic;

namespace WPFHospitalEditor.Controller
{
    public interface IMedicationServerController
    {
        IEnumerable<MedicationDto> GetAllMedication();
        IEnumerable<MedicationDto> GetAllMedicationByName(string name);
    }
}
