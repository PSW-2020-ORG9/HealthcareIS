using Hospital.API.DTOs;
using Hospital.API.Model.Resources;
using System.Collections.Generic;

namespace Hospital.API.Services.Resources
{
    public interface IEquipmentService
    {
        EquipmentUnit GetByID(int id);
        IEnumerable<EquipmentUnit> GetAll();
        EquipmentUnit Create(EquipmentUnit equipmentUnit);
        EquipmentUnit Update(EquipmentUnit equipmentUnit);
        void Delete(EquipmentUnit equipmentUnit);
        void DeleteByType(EquipmentType equipmentType);
        IEnumerable<EquipmentDto> GetEquipmentWithQuantityByRoomId(int roomId);
        IEnumerable<EquipmentDto> GetEquipmentWithQuantityByType(string equipmentType);

    }
}
