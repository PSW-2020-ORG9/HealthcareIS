using RestSharp;
using System;
using WPFHospitalEditor.Service.Interface;
using Newtonsoft.Json;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.DTOs;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service
{
    public class ExaminationServerService : IExaminationServerService
    {
        public string Cancel(int examinationId)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/cancel/" + examinationId, Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            var response = client.Execute(request);
            return response.StatusCode.ToString();
        }

        public IEnumerable<Examination> GetBySpecialtyId(int specialtyId)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/by-specialty/" + specialtyId, Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            var response = client.Get<IEnumerable<Examination>>(request);
            return response.Data;
        }

        public Examination ScheduleEmergencyExamination(DateTime startTime, int doctorId, int patientId)
        {
            ScheduledExaminationDTO examinationDTO = new ScheduledExaminationDTO()
            {
                StartTime = startTime,
                DoctorId = doctorId,
                PatientId = patientId
            };
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/schedule-emergency", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(JsonConvert.SerializeObject(examinationDTO));
            var response = client.Post<Examination>(request);
            return response.Data;
        }

        public Examination ScheduleExamination(DateTime startTime, int doctorId, int patientId)
        {
            ScheduledExaminationDTO examinationDTO = new ScheduledExaminationDTO()
            {
                StartTime = startTime,
                DoctorId = doctorId,
                PatientId = patientId
            };
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(JsonConvert.SerializeObject(examinationDTO));
            var response = client.Post<Examination>(request);
            return response.Data;
        }

        public IEnumerable<Examination> GetByRoomId(int roomId)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/by-room/" + roomId, Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            var response = client.Get< IEnumerable<Examination>>(request);
            return response.Data;
        }
    }
}
