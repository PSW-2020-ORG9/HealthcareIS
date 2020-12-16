using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;
using HealthcareBase.Service.UsersService.EmployeeService;
using HospitalWebApp.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController:ControllerBase
    
    {
        private readonly DoctorService doctorService;

        public DoctorController(DoctorService doctorService)
        {
            this.doctorService = doctorService;
        }
        [HttpGet]
        [Route("specialty/{specialtyId}")]
        public IActionResult GetBySpecialty(int specialtyId)
        {
            if (specialtyId <= 0) return BadRequest("Bad specialty.");
            var doctors = doctorService.GetBySpecialty(specialtyId);
            var doctorDtoList = DoctorMapper.ObjectToDto(doctors);
            doctorDtoList.ToList().ForEach(dto => dto.SpecialtyId = specialtyId);
            return Ok(doctorDtoList);
        }

        [HttpGet]
        [Route("getDoctorsByDepartment/{departmentId}")]
        public IActionResult GetDoctorsByDepartment(int departmentId)
        {
            IEnumerable<DoctorDto> docDtos = doctorService.GetDoctorsByDepartment(departmentId);
            if (docDtos != null) return Ok(docDtos);
            return BadRequest("EquipmentType not found.");
        }

        [HttpGet]
        [Route("getDoctorById/{doctorId}")]
        public IActionResult GetDoctorById(int doctorId)
        {
            Doctor doctor = doctorService.GetByID(doctorId);
            if (doctor != null) return Ok(doctor);
            return BadRequest("Doctor with id: " + doctorId + " not found.");
        }
    }
}