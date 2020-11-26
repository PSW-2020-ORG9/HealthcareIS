using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Service.UsersService.PatientService;
using HospitalWebApp.Adapters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [Route("find/{id}")]
        public IActionResult FindPatient(int id)
        {
            Patient p = _patientService.GetByID(id);
            if (p != null) return Ok(p);
            return BadRequest("Patient not found.");
        }
    }
}
