using Microsoft.AspNetCore.Mvc;
using User.API.Services.EmployeeService;

namespace User.API.Controllers
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