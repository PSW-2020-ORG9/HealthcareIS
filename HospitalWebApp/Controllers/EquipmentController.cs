using HealthcareBase.Service.HospitalResourcesService.EquipmentService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareBase.Model.EditorDtos;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly EquipmentService _equipmentService;

        public EquipmentController(EquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        [Route("getByRoomId/{roomId}")]
        public IActionResult GetEquipmentByRoomId(int roomId)
        {
            IEnumerable<EquipmentDto> eqDtos = _equipmentService.GetEquipmentByRoomId(roomId);
            //IEnumerable<EquipmentDto> eqDtos = EquipmentAdapter.ObjectsToDto(eqUnits);
            if (eqDtos != null) return Ok(eqDtos);
            return BadRequest("Equipment not found.");
        }
    }
}
