using HealthcareBase.Dto;
using RestSharp;
using System.Collections.Generic;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class EquipmentTypeServerService : IEquipmentTypeServerService
    {
        public IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes()
        {
            var client = new RestClient(AllConstants.connectionUrl);
            var request = new RestRequest("EquipmentType/getAll", Method.GET);
            var response = client.Get<IEnumerable<EquipmentTypeDto>>(request);
            return response.Data;
        }
    }
}
