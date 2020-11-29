using HealthcareBase.Model.EditorDtos;
using HealthcareBase.Model.HospitalResources;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.Service
{
   public  class EquipmentServerService
    {
        public IEnumerable<EquipmentDto> getEquipmentByRoomId(int roomId)
        {
            var client = new RestClient("http://localhost:5290/");
            var request = new RestRequest("Equipment/getByRoomId/" + roomId, Method.GET);
            var response = client.Get<List<EquipmentDto>>(request);
            IEnumerable<EquipmentDto> result = response.Data;
            return result;
        }
    }
}
