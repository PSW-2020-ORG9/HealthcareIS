using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using User.API.DTOs;
using User.API.Mappers;
using User.API.Services.EmployeeService;


namespace User.API.Controllers
{
    [ApiController]
    [Route("user/[controller]")]
    public class DoctorController:ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            this._doctorService = doctorService;
        }

        [HttpGet]
        [Route("specialty/{specialtyId}")]
        public IActionResult GetBySpecialty(int specialtyId)
        {
            if (specialtyId <= 0) return BadRequest("Bad specialty.");
            var doctors = _doctorService.GetBySpecialty(specialtyId);
            var doctorDtoList = DoctorMapper.ObjectToDto(doctors);
            doctorDtoList.ToList().ForEach(dto => dto.SpecialtyId = specialtyId);
            return Ok(doctorDtoList);
        }

        [HttpGet]
        [Route("specialty/ids/{specialtyId}")]
        public IActionResult GetIdsBySpecialty(int specialtyId)
        {
            if (specialtyId <= 0) return BadRequest("Bad specialty");
            return Ok(_doctorService.GetIdsBySpecialty(specialtyId));
        }

        [HttpGet]
        [Route("department/{departmentId}")]
        public IActionResult GetDoctorsByDepartment(int departmentId)
        {
            IEnumerable<DoctorDTO> doctorDtoList = _doctorService.GetDoctorsByDepartment(departmentId);
            if (doctorDtoList != null) return Ok(doctorDtoList);
            return BadRequest("Doctors not found.");
        }

        [HttpGet]
        [Route("{doctorId}")]
        public IActionResult GetDoctorById(int doctorId)
        {
            var doctor = _doctorService.GetByID(doctorId);
            if (doctor != null) return Ok(doctor);
            return BadRequest("Doctor with id: " + doctorId + " not found.");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _doctorService.GetAll();
            if (doctors != null) return Ok(doctors);
            return BadRequest("Doctors not found.");
        }

        [HttpPost]
        public IActionResult FindDoctors(IEnumerable<int> doctorIds)
        {
            return Ok(_doctorService.Find(doctorIds));
        }
    }
}