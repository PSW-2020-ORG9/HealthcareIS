using RestSharp;
using System.Collections.Generic;
using System.Linq;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class EquipmentTypeServerService : IEquipmentTypeServerService
    {
        IMapObjectController imoc = new MapObjectController();

        public IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes()
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("api/hospital/equipmenttype", Method.GET);
            var response = client.Get<IEnumerable<EquipmentTypeDto>>(request);
            return response.Data;
        }

        public IEnumerable<EquipmentTypeDto> SearchEquipmentTypes(string name)
        {
            var equipmentTypes = new List<EquipmentTypeDto>();
            List<EquipmentTypeDto> allEquipmentTypes = GetAllEquipmentTypes().ToList();
            if (string.IsNullOrEmpty(name)) return allEquipmentTypes;
            foreach (EquipmentTypeDto equipmentTypeDto in allEquipmentTypes)
            {
                if (CompareInput(equipmentTypeDto, name))
                    equipmentTypes.Add(equipmentTypeDto);
            }
            return equipmentTypes;
        }

        private bool CompareInput(EquipmentTypeDto equipmentTypeDto, string name)
        {
            return equipmentTypeDto.Name.ToLower().Contains(name.ToLower());
        }
    }
}
