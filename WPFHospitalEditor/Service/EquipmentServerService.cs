using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
   public class EquipmentServerService : IEquipmentServerService
    {
        public IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/hospital/equipment/room/" + roomId, Method.GET);
            var response = client.Get<IEnumerable<EquipmentDto>>(request);
            return response.Data;
        }

        public IEnumerable<EquipmentDto> GetEquipmentByType(string equipmentType)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/hospital/equipment/equipment-type/" + equipmentType, Method.GET);
            var response = client.Get<IEnumerable<EquipmentDto>>(request);
            return response.Data;
        }

        public bool RelocateEquipment(EquipmentRelocationDto eqRealDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/hospital/equipment/Equipment/", Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(eqRealDto));
            var response = client.Post<bool>(request);
            return response.Data;
        }
    }
}
