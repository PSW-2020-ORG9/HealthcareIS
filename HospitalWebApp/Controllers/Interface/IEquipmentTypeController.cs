using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers.Interface
{
    public interface IEquipmentTypeController
    {
        public IActionResult GetAllEquipmentTypes();

    }
}
