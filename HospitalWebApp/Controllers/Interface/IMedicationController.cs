using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers.Interface
{
    public interface IMedicationController
    {
        IActionResult GetAllMedication();
        IActionResult GetAllMedicationByName(string name);
    }
}
