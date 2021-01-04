using Hospital.API.DTOs;
using Hospital.API.Services.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hospital.API.Controllers
{
    [ApiController]
    [Route("hospital/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        [Route("getByRoomId/{roomId}")]
        public IActionResult GetEquipmentByRoomId(int roomId)
        {
            IEnumerable<EquipmentDto> eqDtos = _equipmentService.GetEquipmentWithQuantityByRoomId(roomId);
            if (eqDtos != null) return Ok(eqDtos);
            return BadRequest("Equipment not found.");
        }

        [HttpGet]
        [Route("getByEquipmentType/{equipmentType}")]
        public IActionResult GetEquipmentsByType(string equipmentType)
        {
            IEnumerable<EquipmentDto> eqDtos = _equipmentService.GetEquipmentWithQuantityByType(equipmentType);
            if (eqDtos != null) return Ok(eqDtos);
            return BadRequest("Equipment not found.");
        }
    }
}
