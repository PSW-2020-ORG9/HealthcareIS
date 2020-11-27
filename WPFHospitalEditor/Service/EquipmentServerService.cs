using HealthcareBase.Model.HospitalResources;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.Service
{
   public  class EquipmentServerService
    {
        public IEnumerable<EquipmentUnit> getEquipmentByRoomId(int roomId)
        {
            var client = new RestSharp.RestClient("http://localhost:****/");
            var request = new RestRequest("api/Equipment/getByRoomId" + roomId, Method.GET);
            var response = client.Get<List<EquipmentUnit>>(request);
            IEnumerable<EquipmentUnit> result = response.Data;
            return result;
        }
    }
}
