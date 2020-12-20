using RestSharp;
using System;
using WPFHospitalEditor.Service.Interface;
using Newtonsoft.Json;
using HealthcareBase.Model.Schedule.Procedures.DTOs;

namespace WPFHospitalEditor.Service
{
    public class ExaminationServerService : IExaminationServerService
    {
        public string ScheduleExamination(DateTime startTime, int doctorId, int patientId)
        {
            ScheduledExaminationDTO examinationDTO = new ScheduledExaminationDTO()
            {
                StartTime = startTime,
                DoctorId = doctorId,
                PatientId = patientId
            };
            var client = new RestClient(AllConstants.connectionUrl);
            var request = new RestRequest("Examination/", Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(examinationDTO));
            IRestResponse restResponse = client.Execute(request);
            return restResponse.StatusCode.ToString();
        }
    }
}
