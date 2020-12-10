using HealthcareBase.Model.Schedule.Procedures.DTOs;
using HospitalWebApp;
using System;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace HospitalWebAppIntegrationTests
{
    public class ExaminationSchedulingTests
    : IClassFixture<HospitalWebApplicationFactory<TestStartup>>
    {
        private readonly HospitalWebApplicationFactory<TestStartup> _factory;
        public ExaminationSchedulingTests(HospitalWebApplicationFactory<TestStartup> factory)
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

        [Fact]
        public async void Schedule_examination_fail()
        {
            var client = _factory.CreateClient();
            var content = JsonContent.Create(new ScheduledExaminationDTO
            {
                PatientId = 1,
                DoctorId = 1,
                StartTime = new DateTime(2022, 1, 1, 8, 0, 0),
            });

            var response = await client.PostAsync("/examination", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
