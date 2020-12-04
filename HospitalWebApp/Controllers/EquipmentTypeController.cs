using HealthcareBase.Service.HospitalResourcesService.EquipmentService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HealthcareBase.Dto;
using HealthcareBase.Service.HospitalResourcesService.EquipmentService.Interface;
using HospitalWebApp.Controllers.Interface;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentTypeController : ControllerBase, IEquipmentTypeController
    {
        private readonly IEquipmentTypeService _equipmentTypeService;

        public EquipmentTypeController(EquipmentTypeService equipmentTypeService)
        {
            _equipmentTypeService = equipmentTypeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllEquipmentTypes()
        {
            IEnumerable<EquipmentTypeDto> eqTDtos = _equipmentTypeService.GetAllEquipmentTypes();
            if (eqTDtos != null) return Ok(eqTDtos);
            return BadRequest("EquipmentType not found.");
        }

    }
}
