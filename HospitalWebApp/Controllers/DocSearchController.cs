﻿using System;
using System.Linq;
using HospitalWebApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Service.MedicationService;
using Service.ScheduleService.ProcedureService;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocSearchController : ControllerBase
    {
        private readonly MedicationPrescriptionService _medicationPrescriptionService;
        private readonly ExaminationService _examinationService;

        public DocSearchController(
            MedicationPrescriptionService medicationPrescriptionService,
            ExaminationService examinationService)
        {
            _medicationPrescriptionService = medicationPrescriptionService;
            _examinationService = examinationService;
        }

        [HttpGet]
        [Route("prescription/simple")]
        public IActionResult GetPrescriptionByName(string medicationName)
            => Ok(_medicationPrescriptionService.GetByName(medicationName));

        [HttpGet]
        [Route("examination/simple")]
        public IActionResult GetExaminationsByDoctorCredentials(string name, string surname)
        {
            var doctorCredentialsDto = new DoctorCredentialsDto()
            {
                Name = name,
                Surname = surname
            };
            return Ok(_examinationService.GetByDoctorCredentials(doctorCredentialsDto));
        }

        [HttpGet]
        [Route("prescription")]
        public IActionResult GetAllPrescriptions()
            => Ok(_medicationPrescriptionService.GetAll());
        
        [HttpGet]
        [Route("examination")]
        public IActionResult GetAllExaminations()
            => Ok(_examinationService.GetAll());
    }
}