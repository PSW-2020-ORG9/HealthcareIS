﻿using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
   public class EquipmentServerService : IEquipmentServerService
    {
        public IEnumerable<EquipmentDto> GetEquipmentByRoomId(int roomId)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/hospital/equipment/room/" + roomId, Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            var response = client.Get<IEnumerable<EquipmentDto>>(request);
            return response.Data;
        }

        public IEnumerable<EquipmentDto> GetEquipmentByType(string equipmentType)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/hospital/equipment/equipment-type/" + equipmentType, Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            var response = client.Get<IEnumerable<EquipmentDto>>(request);
            return response.Data;
        }

    }
}
