﻿using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
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
            var response = await client.PostAsync("/room", content);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }
    }
}