using HealthcareBase.Dto;
using HealthcareBase.Model.HospitalResources;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthcareBase.Service.HospitalResourcesService.EquipmentService.Interface
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
        Dictionary<int, EquipmentDto> GetEquipmentWithQuantityByType(string equipmentType);

    }
}
