﻿using HealthcareBase.Service.HospitalResourcesService.EquipmentService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HealthcareBase.Dto;

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
            Dictionary<String,EquipmentDto> eqDtos = _equipmentService.GetEquipmentWithQuantityByRoomId(roomId);
            if (eqDtos != null) return Ok(eqDtos);
            return BadRequest("Equipment not found.");
        }
    }
}
