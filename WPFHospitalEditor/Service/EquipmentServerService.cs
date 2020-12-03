using HealthcareBase.Dto;
using RestSharp;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service
{
   public  class EquipmentServerService
    {
        public IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId)
        {
            var client = new RestClient(AllConstants.connectionUrl);
            var request = new RestRequest("Equipment/getByRoomId/" + roomId, Method.GET);
            var response = client.Get<IEnumerable<EquipmentDto>>(request);
            return response.Data;
        }
    }
}
