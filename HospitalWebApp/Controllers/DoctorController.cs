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
        [Route("department/{id}")]
        public IActionResult GetDoctorsByDepartment(int id)
        {
            IEnumerable<DoctorDto> docDtos = doctorService.GetDoctorsByDepartment(id);
            if (docDtos != null) return Ok(docDtos);
            return BadRequest("Doctors not found.");
        }
     
        [HttpGet]
        [Route("find/{id}")]
        public IActionResult GetDoctorById(int id)
        {
            Doctor doctor = doctorService.GetByID(id);
            if (doctor != null) return Ok(doctor);
            return BadRequest("Doctor with id: " + id + " not found.");
        }

        [HttpGet]
        [Route("specialist")]
        public IActionResult GetAllSpecialists()
        {
            IEnumerable<DoctorDto> docDtos = doctorService.GetAllSpecialists();
            if (docDtos != null) return Ok(docDtos);
            return BadRequest("Specialists not found.");
        }
    }
}