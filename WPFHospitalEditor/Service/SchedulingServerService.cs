using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
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

        public List<EquipmentRelocationDto> GetEquipmentRelocationAppointments(EquipmentRecommendationRequestDto equipmentRecommendationRequestDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/recommend-equipment-relocation", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(RecommendationEquipmentRelocationDtoToJson(equipmentRecommendationRequestDto));
            var response = client.Post<List<EquipmentRelocationDto>>(request);
            return response.Data;
        }

        private String RecommendationDtoToJson(RecommendationRequestDto recDto)
        {
            return JsonConvert.SerializeObject(recDto);
        }

        private String RecommendationEquipmentRelocationDtoToJson(EquipmentRecommendationRequestDto equipmentRecomendationRequestDto)
        {
            return JsonConvert.SerializeObject(equipmentRecomendationRequestDto);
        }
    }
}
