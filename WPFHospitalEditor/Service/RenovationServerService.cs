﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class RenovationServerService : IRenovationServerService
    {
        public string ScheduleRenovation(TimeInterval timeInterval, int doctorId, int patientId)
        {
            ScheduleRenovationDTO scheduleRenovationDTO = new ScheduleRenovationDTO()
            {
                TimeInterval = new TimeInterval(timeInterval.Start, timeInterval.End),
                DoctorId = doctorId,
                PatientId = patientId
            };
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/schedule-renovation", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(JsonConvert.SerializeObject(scheduleRenovationDTO));
            var response = client.Execute(request);
            return response.StatusCode.ToString();
        }
    }
}
