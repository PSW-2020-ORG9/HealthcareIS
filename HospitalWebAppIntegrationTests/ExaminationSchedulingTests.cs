using HealthcareBase.Model.Schedule.Procedures.DTOs;
using HospitalWebApp;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using Xunit;

namespace HospitalWebAppIntegrationTests
{
    public class ExaminationSchedulingTests
    : IClassFixture<HospitalWebApplicationFactory<Startup>>
    {
        private readonly HospitalWebApplicationFactory<Startup> _factory;
        public ExaminationSchedulingTests(HospitalWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Schedule_examination_success()
        {
            var client = _factory.CreateClient();
            var content = JsonContent.Create(new ScheduledExaminationDTO
            {
                PatientId = 1,
                DoctorId = 1,
                StartTime = new DateTime(2022, 1, 1, 9, 0, 0)
            });

            var response = await client.PostAsync("/examination", content);
            string responseData = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"patientId\":1", responseData);
        }
    }
}
