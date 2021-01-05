using RestSharp;
using System;
using WPFHospitalEditor.Service.Interface;
using Newtonsoft.Json;
using HealthcareBase.Model.Schedule.Procedures.DTOs;
using HealthcareBase.Model.Schedule.Procedures;

namespace WPFHospitalEditor.Service
{
    public class ExaminationServerService : IExaminationServerService
    {
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
            request.AddJsonBody(JsonConvert.SerializeObject(examinationDTO));
            var response = client.Post<Examination>(request);
            return response.Data;
        }
    }
}
