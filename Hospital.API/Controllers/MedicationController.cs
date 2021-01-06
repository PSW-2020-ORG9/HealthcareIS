using Hospital.API.DTOs;
using Hospital.API.Services.Medications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hospital.API.Controllers
{
    [ApiController]
    [Route("hospital/[controller]")]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationService _medicationService;

        public MedicationController(IMedicationService medicationService)
        {
            _medicationService= medicationService;
        }

        [HttpGet]
        public IActionResult GetAllMedication()
        {
            IEnumerable<MedicationDto> medDtos = _medicationService.GetAllMedicationsWithQuantity();
            if (medDtos != null) return Ok(medDtos);
            return BadRequest("Medication not found.");
        }

        [HttpGet]
        [Route("by-name/{name}")]
        public IActionResult GetAllMedicationByName(string name)
        {
            IEnumerable<MedicationDto> medDtos = _medicationService.GetAllMedicationWithQuantityByName(name);
            if (medDtos != null) return Ok(medDtos);
            return BadRequest("Medication not found.");
        }
    }
}
