using System;
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
        [Route("prescription/simple/{medicationNameQuery}")]
        public IActionResult GetNameContainedPrescription(string medicationNameQuery)
            => Ok(_medicationPrescriptionService.GetNameContained(medicationNameQuery));
        
        [HttpPost]
        [Route("examination/simple")]
        public IActionResult GetExaminationsByDoctorName(DoctorCredentialsDto doctorCredentialsDto)
        {
            Console.WriteLine(doctorCredentialsDto.Name + " " + doctorCredentialsDto.Surname);
            try
            {
                return Ok(_examinationService.GetByDoctorCredentials(doctorCredentialsDto));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}