using RestSharp;
using System.Collections.Generic;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class RoomServerService : IRoomServerService
    {
        public IEnumerable<Room> GetRoomsByEquipmentType(string equipmentType)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/hospital/room/equipment-type/" + equipmentType, Method.GET);
            var response = client.Get<IEnumerable<Room>>(request);
            return response.Data;
        }
    }
}
