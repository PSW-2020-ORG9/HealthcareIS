using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using User.API.DTOs;
using User.API.Mappers;
using User.API.Model.Users.Employees.Doctors;
using User.API.Services.EmployeeService;


namespace User.API.Controllers
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
        [Route("department/{departmentId}")]
        public IActionResult GetDoctorsByDepartment(int departmentId)
        {
            IEnumerable<DoctorDTO> docDtos = doctorService.GetDoctorsByDepartment(departmentId);
            if (docDtos != null) return Ok(docDtos);
            return BadRequest("Doctors not found.");
        }
     
        [HttpGet]
        [Route("{doctorId}")]
        public IActionResult GetDoctorById(int doctorId)
        {
            var doctor = doctorService.GetByID(doctorId);
            if (doctor != null) return Ok(doctor);
            return BadRequest("Doctor with id: " + doctorId + " not found.");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = doctorService.GetAll();
            if (doctors != null) return Ok(doctors);
            return BadRequest("Doctors not found.");

        }
    }
}