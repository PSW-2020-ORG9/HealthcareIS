using HealthcareBase.Dto;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service
{
    public interface IMedicationServerService
    {
        IEnumerable<MedicationDto> GetAllMedication();
    }
}
