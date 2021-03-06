﻿using System;
using System.Collections.Generic;
using System.IO;
using General.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using User.API.Mappers;
using User.API.Model.Users.UserAccounts.Registration;
using User.API.Services.PatientService;
using User.API.Services.RegistrationService;

namespace User.API.Controllers
{
    [ApiController]
    [Route("user/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly IPatientRegistrationService _patientRegistrationService;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IPatientAccountService _patientAccountService;

        public PatientController(PatientService patientService,
            IPatientRegistrationService patientRegistrationService,
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
        [Route("{id}")]
        public IActionResult FindPatient(int id)
        {
            var patient = _patientService.GetByID(id);
            if (patient != null) return Ok(patient);
            return BadRequest("Patient not found.");
        }

        [HttpGet]
        public IActionResult FindPatient()
        {
            string userId = HttpIdentityHandler.GetUserIdFromRequest(HttpContext.Request);
            if (userId != default)
            {
                return Ok(_patientService.GetByID(Int32.Parse(userId)));
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("username")]
        public IActionResult FindPatientByUsername()
        {
            var patient = _patientService
                .GetByUsername(HttpIdentityHandler.GetUsernameFromRequest(HttpContext.Request));
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
            return RedirectPermanent("http://localhost:8080/#/successfully-registered");
        }

        [HttpGet]
        [Route("account")]
        public IActionResult FindPatientAccount()
        {
            var patientAccount = _patientAccountService
                .GetAccount(Int32.Parse(HttpIdentityHandler.GetUserIdFromRequest(HttpContext.Request)));
            if(patientAccount != null) return Ok(patientAccount);
            return BadRequest("Patient account not found.");

        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllPatients()
        {
            var patients = _patientService.GetAllActive();
            if (patients != null) return Ok(patients);
            return BadRequest("Patients not found.");
        }

        [HttpPost]
        [Route("accounts")]
        public IActionResult FindPatientAccounts(IEnumerable<int> patientIds)
        {
            var patientAccounts = _patientAccountService.FindAccounts(patientIds);
            if (patientAccounts != null) return Ok(patientAccounts);
            return BadRequest("Patient accounts not found.");
        }
        
        [HttpPost]
        public IActionResult FindPatients(IEnumerable<int> patientIds)
        {
            return Ok(_patientService.Find(patientIds));
        }
            
    }
}
