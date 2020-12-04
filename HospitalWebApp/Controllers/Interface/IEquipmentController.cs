using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalWebApp.Controllers.Interface
{
    public interface IEquipmentController
    {
        IActionResult GetEquipmentByRoomId(int roomId);
        IActionResult GetEquipmentsByType(string equipmentType);
    }
}
