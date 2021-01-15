﻿using Newtonsoft.Json;
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
        public IEnumerable<Room> GetRoomsByEquipmentType(string equipmentType)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/hospital/room/equipment-type/" + equipmentType, Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            var response = client.Get<IEnumerable<Room>>(request);
            return response.Data;
        }

        public IEnumerable<int> GetUnavailableRoomsIdsInTimeInterval(EquipmentRelocationDto eqRelDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/check-rooms", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(EquipmentRelocationDtoToJson(eqRelDto));
            var response = client.Post<List<int>>(request);
            return response.Data;
        }

        private String EquipmentRelocationDtoToJson(EquipmentRelocationDto eqRelDto)
        {
            return JsonConvert.SerializeObject(eqRelDto);
        }
    }
}