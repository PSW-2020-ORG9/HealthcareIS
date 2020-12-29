using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using Xunit;

namespace Schedule.API.IntegrationTests
{
    public class DiagnosisTests
        : IClassFixture<ScheduleApiFactory<TestStartup>>
    {
        private readonly ScheduleApiFactory<TestStartup> _factory;
        public DiagnosisTests(ScheduleApiFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Finds_diagnosis()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/diagnosis/1");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }

        [Fact]
        public async void Finds_multiple_diagnoses()
        {
            var client = _factory.CreateClient();
            List<int> diagnosisIds = new List<int>();
            diagnosisIds.Add(1);
            var content = JsonContent.Create(diagnosisIds);
            var response = await client.PostAsync("/diagnosis", content);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }
    }
}
