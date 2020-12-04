using HealthcareBase.Dto;
using HealthcareBase.Model.HospitalResources;
using System.Collections.Generic;

namespace HealthcareBase.Service.HospitalResourcesService.EquipmentService.Interface
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
