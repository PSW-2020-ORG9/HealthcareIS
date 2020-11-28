﻿using System;
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
using Microsoft.Extensions.Hosting;
using PatientRegistrationService = HealthcareBase.Service.UsersService.RegistrationService.PatientRegistrationService;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly PatientRegistrationService patientRegistrationService;
        private readonly IHostEnvironment _hostEnvironment;

        public PatientController(PatientService patientService,PatientRegistrationService patientRegistrationService,IHostEnvironment _hostEnvironment
        )
        {
            this._hostEnvironment = _hostEnvironment;
            _patientService = patientService;
            this.patientRegistrationService = patientRegistrationService;
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
            Console.WriteLine(emailTemplatePath);
            patientRegistrationService.RegisterPatient(patientAccount,emailTemplatePath);
            return Ok();
        }

        [HttpGet]
        [Route("activate/{id}")]
        public IActionResult ActivatePatient(int id)
        {
            patientRegistrationService.ActivatePatient(id);
            return Redirect("http://localhost:8080/#/successfully-registered");

        }
    }
}
