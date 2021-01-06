using Hospital.API.DTOs;
using System.Collections.Generic;

namespace Hospital.API.Services.Medications
{
    public interface IMedicationService
    {
        IEnumerable<MedicationDto> GetAllMedicationsWithQuantity();
        IEnumerable<MedicationDto> GetAllMedicationWithQuantityByName(string name);
    }
}
