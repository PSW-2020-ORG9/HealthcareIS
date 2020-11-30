using HealthcareBase.Dto;
using RestSharp;
using System;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service
{
   public  class EquipmentServerService
    {
        public Dictionary<String,EquipmentDto> getEquipmentByRoomId(int roomId)
        {
            var client = new RestClient(AllConstants.connectionUrl);
            var request = new RestRequest("Equipment/getByRoomId/" + roomId, Method.GET);
            var response = client.Get<Dictionary<String,EquipmentDto>>(request);
            IRestResponse restResponse = client.Execute(request);
            return response.Data;
        }
    }
}
