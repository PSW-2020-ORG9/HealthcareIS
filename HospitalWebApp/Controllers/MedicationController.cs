using HealthcareBase.Dto;
using HealthcareBase.Service.MedicationService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicationController :ControllerBase
    {
        private readonly MedicationService _medicationService;

        public MedicationController(MedicationService medicationService)
        {
            this._medicationService= medicationService;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAllMedication()
        {
            IEnumerable<MedicationDto> medDtos = _medicationService.GetAllMedicineWithQuantity();
            if (medDtos != null) return Ok(medDtos);
            return BadRequest("Medication not found.");
        }
    }
}
