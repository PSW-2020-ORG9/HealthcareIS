using Hospital.API.DTOs;
using Hospital.API.Model.Resources;
using System.Collections.Generic;

namespace Hospital.API.Services.Resources
{
    public interface IEquipmentTypeService
    {
        EquipmentType GetByID(int id);
        IEnumerable<EquipmentType> GetAll();
        EquipmentType Create(EquipmentType equipmentType);
        EquipmentType Update(EquipmentType equipmentType);
        void Delete(EquipmentType equipmentType);
        IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes();
    }
}
