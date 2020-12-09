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
    public class EquipmentController : ControllerBase, IEquipmentController
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(EquipmentService equipmentService)
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
