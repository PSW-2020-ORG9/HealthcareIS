using HealthcareBase.Dto;
using System.Collections.Generic;

namespace HealthcareBase.Service.MedicationService.Interface
{
    public interface IMedicationService
    {
        IEnumerable<MedicationDto> GetAllMedicationsWithQuantity();
    }
}
