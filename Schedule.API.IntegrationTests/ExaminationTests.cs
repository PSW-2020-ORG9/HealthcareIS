using Schedule.API.DTOs;
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
            var response = await client.GetAsync("schedule/examination/patient/1");
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
                PatientId = 1,
                StartTime = new DateTime(2022, 3, 3, 8, 0, 0)
            });
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

        [Fact]
        public async void Check_rooms_availability_with_overlaps()
        {
            var client = _factory.CreateClient();
            var content = JsonContent.Create(new EquipmentRelocationDto
            {
                SourceRoomId = 2,
                DestinationRoomId = 1,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2022, 3, 3, 9, 0, 0),
                    End = new DateTime(2022, 3, 3, 10, 30, 0)
                },
                Amount = 5,
                EquipmentType = "Chair"
            });
            var response = await client.PostAsync("schedule/examination/check-rooms", content);
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("1", responseString);
        }

        [Fact]
        public async void Check_rooms_availability_without_overlaps()
        {
            var client = _factory.CreateClient();
            var content = JsonContent.Create(new EquipmentRelocationDto
            {
                SourceRoomId = 2,
                DestinationRoomId = 1,
                TimeInterval = new TimeInterval
                {
                    Start = new DateTime(2022, 11, 11, 9, 0, 0),
                    End = new DateTime(2022, 11, 11, 11, 30, 0)
                },
                Amount = 5,
                EquipmentType = "Chair"
            });
            var response = await client.PostAsync("schedule/examination/check-rooms", content);
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("", responseString);
        }
    }
}
