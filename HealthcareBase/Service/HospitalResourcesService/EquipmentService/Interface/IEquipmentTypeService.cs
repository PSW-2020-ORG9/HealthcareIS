using HealthcareBase.Model.HospitalResources;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthcareBase.Service.HospitalResourcesService.EquipmentService.Interface
{
    public interface IEquipmentTypeService
    {
        EquipmentType GetByID(int id);
        IEnumerable<EquipmentType> GetAll();
        EquipmentType Create(EquipmentType equipmentType);
        EquipmentType Update(EquipmentType equipmentType);
        void Delete(EquipmentType equipmentType);
    }
}
