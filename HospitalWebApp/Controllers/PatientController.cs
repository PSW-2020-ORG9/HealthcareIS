using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Model.Users.UserAccounts.Registration;
using HealthcareBase.Service.UsersService.PatientService;
using HospitalWebApp.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientRegistrationService = HealthcareBase.Service.UsersService.RegistrationService.PatientRegistrationService;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly PatientRegistrationService patientRegistrationService;

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

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterPatient(PatientRegistrationDTO dto)
        {
            var patientAccount = PatientAccountMapper.DtoToObject(dto);
            var emailTemplatePath = Path.Join("..","Resources","verification-mail.html");
            patientRegistrationService.RegisterPatient(patientAccount,emailTemplatePath);
            return Ok();
        }

        [HttpGet]
        [Route("/activate/{guid}")]
        public IActionResult ActivatePatient(Guid guid)
        {
            patientRegistrationService.ActivatePatient(guid);
            return Redirect("http://localhost:8000/register/activated");

        }
    }
}
