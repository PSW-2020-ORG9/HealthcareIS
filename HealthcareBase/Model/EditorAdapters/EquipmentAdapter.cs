using HealthcareBase.Model.EditorDtos;
using HealthcareBase.Model.HospitalResources;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthcareBase.Model.EditorAdapters
{
    public class EquipmentAdapter
    {
        public static EquipmentUnit DtoToObject(EquipmentDto eqDto)
        {
            return new EquipmentUnit
            {


            };
        }

        public static EquipmentDto ObjectToDto(EquipmentUnit eqUnit)
        {
            return new EquipmentDto
            {


            };
        }

        public static IEnumerable<EquipmentDto> ObjectsToDto(IEnumerable<EquipmentUnit> eqUnits)
        {
            List<EquipmentDto> eqDtos = new List<EquipmentDto>();
            foreach (EquipmentUnit eqUnit in eqUnits)
            {
               
            }

            return eqDtos;
        }
    }
}
