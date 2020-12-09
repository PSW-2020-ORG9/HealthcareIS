﻿using HealthcareBase.Model.Filters;
using HealthcareBase.Service.MedicationService;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult PrescriptionSimpleSearch(string medicationName)
            => Ok(_medicationPrescriptionService.SimpleSearch(medicationName));

        [HttpGet]
        [Route("examination/simple")]
        public IActionResult ExaminationSimpleSearch(string name, string surname)
        {
            var filterDto = new ExaminationSimpleFilterDto()
            {
                Name = name,
                Surname = surname
            };
            return Ok(_examinationService.SimpleSearch(filterDto));
        }

        [HttpPost]
        [Route("prescription/advanced")]
        public IActionResult PrescriptionAdvancedSearch(PrescriptionAdvancedFilterDto filterDto)
            => Ok(_medicationPrescriptionService.AdvancedSearch(filterDto));

        [HttpPost]
        [Route("examination/advanced")]
        public IActionResult ExaminationAdvancedSearch(ExaminationAdvancedFilterDto filterDto)
            => Ok(_examinationService.AdvancedSearch(filterDto));
        
        [HttpGet]
        [Route("prescription")]
        public IActionResult GetAllPrescriptions()
            => Ok(_medicationPrescriptionService.GetAll());
        
        [HttpGet]
        [Route("examination/{patientId}")]
        public IActionResult GetPatientExaminations(int patientId)
            => Ok(_examinationService.GetByPatientId(patientId));
    }
}