using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using User.API.Mappers;
using User.API.Model.Users.UserAccounts.Registration;
using User.API.Services.PatientService;
using User.API.Services.RegistrationService;

namespace User.API.Controllers
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
            var patient = _patientService.GetByID(id);
            if (patient != null) return Ok(patient);
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
            var patientAccount = _patientAccountService.GetAccount(id);
            if(patientAccount != null) return Ok(patientAccount);
            return BadRequest("Patient account not found.");

        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = _patientService.GetAllActive();
            if (patients != null) return Ok(patients);
            return BadRequest("Patients not found.");
        }
    }
}
