using HealthcareBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers.Interface
{
    public interface IEquipmentController
    {
        IActionResult GetEquipmentByRoomId(int roomId);
        IActionResult GetEquipmentsByType(string equipmentType);
        IActionResult RelocateEquipment(EquipmentRelocationDto equipmentRelocationDto);
    }
}
