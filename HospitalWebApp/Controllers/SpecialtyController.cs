using HealthcareBase.Service.UsersService.EmployeeService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialtyController:ControllerBase
    {
        private readonly SpecialtyService specialtyService;

        public SpecialtyController(SpecialtyService specialtyService)
        {
            this.specialtyService = specialtyService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(specialtyService.GetAll());
        }
    }
}