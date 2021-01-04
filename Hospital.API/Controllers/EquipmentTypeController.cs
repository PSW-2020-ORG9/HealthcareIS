
using Hospital.API.DTOs;
using Hospital.API.Services.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hospital.API.Controllers
{
    [ApiController]
    [Route("hospital/[controller]")]
    public class EquipmentTypeController : ControllerBase
    {
        private readonly IEquipmentTypeService _equipmentTypeService;

        public EquipmentTypeController(IEquipmentTypeService equipmentTypeService)
        {
            _equipmentTypeService = equipmentTypeService;
        }

        [HttpGet]
        public IActionResult GetAllEquipmentTypes()
        {
            IEnumerable<EquipmentTypeDto> eqTDtos = _equipmentTypeService.GetAllEquipmentTypes();
            if (eqTDtos != null) return Ok(eqTDtos);
            return BadRequest("EquipmentType not found.");
        }

    }
}
