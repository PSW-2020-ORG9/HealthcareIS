using Schedule.API.Model.Procedures.DTOs;
using Schedule.API.Model.Recommendations;
using Schedule.API.Model.Utilities;
using System;
using System.Net.Http.Json;
using Xunit;

namespace Schedule.API.IntegrationTests
{
    public class ExaminationTests
        : IClassFixture<ScheduleApiFactory<TestStartup>>
    {
        private readonly ScheduleApiFactory<TestStartup> _factory;
        public ExaminationTests(ScheduleApiFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Finds_examinations_for_patient()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("UserId", "1");
            var response = await client.GetAsync("schedule/examination");
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"patientId\":1", responseString);
        }

        [Fact]
        public async void Cancels_examination()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("schedule/examination/cancel/2");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void Schedules_examination()
        {
            var client = _factory.CreateClient();
            var content = JsonContent.Create(new ScheduledExaminationDTO
            {
                DoctorId = 1,
                StartTime = new DateTime(2022, 3, 3, 8, 0, 0)
            });
            content.Headers.Add("UserId", "1");
            var response = await client.PostAsync("schedule/examination", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void Gets_examination_recommendation()
        {
            var client = _factory.CreateClient();
            var content = JsonContent.Create(new RecommendationRequestDto
            {
                DoctorId = 1,
                Preference = RecommendationPreference.Doctor,
                SpecialtyId = 1,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2022, 3, 3),
                    End = new DateTime(2022, 3, 4)
                }
            });
            var response = await client.PostAsync("schedule/examination/recommend", content);
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }
    }
}
