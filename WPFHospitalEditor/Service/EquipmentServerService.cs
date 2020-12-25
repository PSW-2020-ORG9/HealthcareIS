using HealthcareBase.Dto;
using RestSharp;
using System.Collections.Generic;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
   public class EquipmentServerService : IEquipmentServerService
    {
        public IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("Equipment/getByRoomId/" + roomId, Method.GET);
            var response = client.Get<IEnumerable<EquipmentDto>>(request);
            return response.Data;
        }

        public IEnumerable<EquipmentDto> GetEquipmentByType(string equipmentType)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("Equipment/getByEquipmentType/" + equipmentType, Method.GET);
            var response = client.Get<IEnumerable<EquipmentDto>>(request);
            return response.Data;
        }

    }
}
