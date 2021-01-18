using Microsoft.AspNetCore.Mvc;
using System;
using User.API.Services.EmployeeService;

namespace User.API.Controllers
{
    [ApiController]
    [Route("user/[controller]")]
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