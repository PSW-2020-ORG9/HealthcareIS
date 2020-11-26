using System;
using System.Linq;
using HealthcareBase.Model.Filters;
using HealthcareBase.Service.MedicationService;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using HospitalWebApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

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
        public IActionResult PrescriptionAdvancedSearch(PrescriptionAdvancedFilterDto dto)
            => Ok(_medicationPrescriptionService.AdvancedSearch(dto));

        [HttpPost]
        [Route("examination/advanced")]
        public IActionResult ExaminationAdvancedSearch(ExaminationAdvancedFilterDto dto)
            => Ok(_examinationService.AdvancedSearch(dto));
        
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