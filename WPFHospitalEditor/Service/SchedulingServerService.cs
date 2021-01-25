using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Windows;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class SchedulingServerService : ISchedulingServerService
    {
        public List<RecommendationDto> GetAppointments(RecommendationRequestDto recDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/recommend", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(RecommendationDtoToJson(recDto));
            var response = client.Post<List<RecommendationDto>>(request);
            return response.Data;
        }

        public List<EquipmentRelocationDto> GetEquipmentRelocationAppointments(SchedulingDto scheduleDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/recommend-equipment-relocation", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(RecommendationEquipmentRelocationDtoToJson(scheduleDto));
            var response = client.Post<List<EquipmentRelocationDto>>(request);
            return response.Data;
        }
        public List<RecommendationDto> GetEmergencyAppointments(RecommendationRequestDto recDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/emergency", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(RecommendationDtoToJson(recDto));
            var response = client.Post<List<RecommendationDto>>(request);
            return response.Data;
        }

        public List<RenovationDto> GetRenovationAppointments(SchedulingDto scheduleDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/recommend-renovation-appointment", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(RecommendationEquipmentRelocationDtoToJson(scheduleDto));
            var response = client.Post<List<RenovationDto>>(request);
            return response.Data;
        }

        private String RecommendationDtoToJson(RecommendationRequestDto recDto)
        {
            return JsonConvert.SerializeObject(recDto);
        }

        private String RecommendationEquipmentRelocationDtoToJson(SchedulingDto equipmentRecomendationRequestDto)
        {
            return JsonConvert.SerializeObject(equipmentRecomendationRequestDto);
        }
    }
}
