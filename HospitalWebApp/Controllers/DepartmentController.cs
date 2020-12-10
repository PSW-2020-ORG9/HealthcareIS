using HealthcareBase.Service.HospitalResourcesService.RoomService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController:ControllerBase
    {
        private readonly DepartmentService departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(departmentService.GetAll());
        }
    }
}