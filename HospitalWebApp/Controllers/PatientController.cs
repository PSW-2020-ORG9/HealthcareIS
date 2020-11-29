using System;
using System.IO;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Model.Users.UserAccounts.Registration;
using HealthcareBase.Service.UsersService.PatientService;
using HealthcareBase.Service.UsersService.RegistrationService;
using HospitalWebApp.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;


namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly PatientRegistrationService _patientRegistrationService;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IPatientAccountService _patientAccountService;

        public PatientController(PatientService patientService,
            PatientRegistrationService patientRegistrationService,
            IHostEnvironment hostEnvironment,
            IPatientAccountService patientAccountService
        )
        {
            _hostEnvironment = hostEnvironment;
            _patientService = patientService;
            _patientRegistrationService = patientRegistrationService;
            _patientAccountService = patientAccountService;
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
            var emailTemplatePath = Path.Join(_hostEnvironment.ContentRootPath,"Resources","verification-mail.html");
            _patientRegistrationService.RegisterPatient(patientAccount,emailTemplatePath);
            return Ok();
        }

        [HttpGet]
        [Route("activate/{guid}")]
        public IActionResult ActivatePatient(Guid guid)
        {
            _patientRegistrationService.ActivatePatient(guid);
            return Redirect("http://localhost:8080/#/successfully-registered");

        }

        [HttpGet]
        [Route("account/{id}")]
        public IActionResult FindPatientAccount(int id)
        {
            PatientAccount pa = _patientAccountService.GetAccount(id);
            if(pa != null) return Ok(pa);
            return BadRequest("Patient account not found.");

        }
    }
}
