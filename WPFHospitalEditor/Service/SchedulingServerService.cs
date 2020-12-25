using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HospitalWebApp.Dtos;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class SchedulingServerService : ISchedulingServerService
    {
        public List<RecommendationDto> GetAppointments(RecommendationRequestDto recDto)
        {
            var client = new RestClient(AllConstants.connectionUrl);
            var request = new RestRequest("Examination/recommend", Method.POST);
            request.AddJsonBody(RecommendationDtoToJson(recDto));
            var response = client.Post<List<RecommendationDto>>(request);
            return response.Data;
        }

        private String RecommendationDtoToJson(RecommendationRequestDto recDto)
        {
            return JsonConvert.SerializeObject(recDto);
        }
    }
}
