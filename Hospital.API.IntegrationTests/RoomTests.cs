using System.Collections.Generic;
using System.Net.Http.Json;
using Xunit;

namespace Hospital.API.IntegrationTests
{
    public class RoomTests
        : IClassFixture<HospitalApiFactory<TestStartup>>
    {
        private readonly HospitalApiFactory<TestStartup> _factory;
        public RoomTests(HospitalApiFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Finds_rooms_by_ids()
        {
            var client = _factory.CreateClient();
            List<int> ids = new List<int>();
            ids.Add(1);
            var content = JsonContent.Create(ids);
            var response = await client.PostAsync("hospital/room", content);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }

        [Fact]
        public async void Finds_rooms_by_equipment_type()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("hospital/room/equipment-type/Chair");
            var responseString = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.Contains("\"id\":1", responseString);
        }
    }
}
