using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Utilities;
using HospitalWebApp.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using Xunit;

namespace HospitalWebAppIntegrationTests
{
    public class ExaminationRecommendationTests
        : IClassFixture<HospitalWebApplicationFactory<TestStartup>>
    {
        private readonly HospitalWebApplicationFactory<TestStartup> _factory;
        public ExaminationRecommendationTests(HospitalWebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Gets_doctor_priority_recommendation()
        {
            var client = _factory.CreateClient();

            var content = JsonContent.Create(new RecommendationRequestDto
            {
                DoctorId = 1,
                Preference = RecommendationPreference.Doctor,
                SpecialtyId = 1,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2022, 1, 2, 0, 0, 0),
                    End = new DateTime(2022, 1, 3, 0, 0, 0)
                }
            });

            var response = await client.PostAsync("/examination/recommend", content);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"start\":\"2022-01-02T08:00:00\",\"end\":\"2022-01-02T08:30:00\"", responseContent);
        }

        [Fact]
        public async void Gets_date_priority_recommendation()
        {
            var client = _factory.CreateClient();

            var content = JsonContent.Create(new RecommendationRequestDto
            {
                DoctorId = 2,
                Preference = RecommendationPreference.Time,
                SpecialtyId = 1,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2022, 1, 2, 0, 0, 0),
                    End = new DateTime(2022, 1, 3, 0, 0, 0)
                }
            });

            var response = await client.PostAsync("/examination/recommend", content);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"start\":\"2022-01-02T08:00:00\",\"end\":\"2022-01-02T08:30:00\"", responseContent);
        }
    }
}
