using Microsoft.AspNetCore.Mvc;
using Service.MedicationService;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocSearchController : ControllerBase
    {
        private readonly MedicationPrescriptionService _medicationPrescriptionService;

        public DocSearchController(MedicationPrescriptionService medicationPrescriptionService) 
            => _medicationPrescriptionService = medicationPrescriptionService;

        [HttpGet]
        [Route("prescription/simple/{medicationNameQuery}")]
        public IActionResult GetNameContainedPrescription(string medicationNameQuery)
            => Ok(_medicationPrescriptionService.GetNameContained(medicationNameQuery));
    }
}