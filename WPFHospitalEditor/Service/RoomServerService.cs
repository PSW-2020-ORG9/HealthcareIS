using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class RoomServerService : IRoomServerService
    {
        public bool CreateRoom(CreateRoomDto createRoomDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/hospital/room/create-room", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(JsonConvert.SerializeObject(createRoomDto));
            var response = client.Post<bool>(request);
            return response.Data;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/hospital/room/get-all", Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);

            var response = client.Get<IEnumerable<Room>>(request);
            return response.Data;
        }

        public IEnumerable<Room> GetRoomsByEquipmentType(string equipmentType)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/hospital/room/equipment-type/" + equipmentType, Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            var response = client.Get<IEnumerable<Room>>(request);
            return response.Data;
        }

        public IEnumerable<int> GetUnavailableRooms(SchedulingDto schedulingDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/unavailable-rooms", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(SchedulingDtoToJson(schedulingDto));
            var response = client.Post<List<int>>(request);
            return response.Data;
        }

        private String SchedulingDtoToJson(SchedulingDto schedulingDto)
        {
            return JsonConvert.SerializeObject(schedulingDto);
        }
    }
}