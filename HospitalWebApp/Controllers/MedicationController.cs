using HealthcareBase.Dto;
using HealthcareBase.Service.MedicationService;
using HealthcareBase.Service.MedicationService.Interface;
using HospitalWebApp.Controllers.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicationController : ControllerBase, IMedicationController
    {
        private readonly IMedicationService _medicationService;

        public MedicationController(MedicationService medicationService)
        {
            this._medicationService= medicationService;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAllMedication()
        {
            IEnumerable<MedicationDto> medDtos = _medicationService.GetAllMedicationsWithQuantity();
            if (medDtos != null) return Ok(medDtos);
            return BadRequest("Medication not found.");
        }
    }
}
