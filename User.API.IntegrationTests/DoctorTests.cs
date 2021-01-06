using System.Collections.Generic;
using System.Net.Http.Json;
using Xunit;

namespace User.API.IntegrationTests
{
    public class DoctorTests
        : IClassFixture<UserApiFactory<TestStartup>>
    {
        private readonly UserApiFactory<TestStartup> _factory;
        public DoctorTests(UserApiFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Gets_all_doctors()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("user/doctor");
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }

        [Fact]
        public async void Gets_doctor_by_specialty()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("user/doctor/specialty/1");
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"doctorId\":1", responseString);
        }

        [Fact]
        public async void Gets_doctor_by_department()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("user/doctor/departments/1");
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"doctorId\":1", responseString);
        }

        [Fact]
        public async void Gets_doctor_by_id()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("user/doctor/1");
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }

        [Fact]
        public async void Gets_doctors_by_ids()
        {
            var client = _factory.CreateClient();
            List<int> doctorIds = new List<int>();
            doctorIds.Add(1);
            var content = JsonContent.Create(doctorIds);
            var response = await client.PostAsync("user/doctor", content);
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }

        [Fact]
        public async void Gets_all_specialties()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("user/specialty");
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }
    }
}
