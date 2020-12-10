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
        public IActionResult GetAll([FromQuery] int department)
        {
            return Ok(DoctorMapper.ObjectToDto(doctorService.GetByDepartment(department)));
        }
    }
}