using Hospital.API.DTOs.Filters;
using Hospital.API.Services.Medications;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocSearchController : ControllerBase
    {
        private readonly IMedicationPrescriptionService _medicationPrescriptionService;

        public DocSearchController(
            IMedicationPrescriptionService medicationPrescriptionService)
        {
            _medicationPrescriptionService = medicationPrescriptionService;
        }
                
        [HttpGet]
        [Route("prescription")]
        public IActionResult GetAllPrescriptions()
            => Ok(_medicationPrescriptionService.GetAll());

        [HttpGet]
        [Route("prescription/simple")]
        public IActionResult PrescriptionSimpleSearch(string medicationName)
            => Ok(_medicationPrescriptionService.SimpleSearch(medicationName));


        [HttpPost]
        [Route("prescription/advanced")]
        public IActionResult PrescriptionAdvancedSearch(PrescriptionAdvancedFilterDto filterDto)
            => Ok(_medicationPrescriptionService.AdvancedSearch(filterDto));
    }
}