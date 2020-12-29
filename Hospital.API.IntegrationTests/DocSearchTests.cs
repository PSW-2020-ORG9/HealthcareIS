using Hospital.API.DTOs.Filters;
using System;
using System.Net.Http.Json;
using Xunit;

namespace Hospital.API.IntegrationTests
{
    public class DocSearchTests
        : IClassFixture<HospitalApiFactory<TestStartup>>
    {
        private readonly HospitalApiFactory<TestStartup> _factory;
        public DocSearchTests(HospitalApiFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Gets_all_prescriptions()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/docsearch/prescription");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Brufen", responseString);
        }

        [Fact]
        public async void Gets_prescription_by_medication_name()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/docsearch/prescription/simple?medicationName=Brufen");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Brufen", responseString);
        }

        [Fact]
        public async void Gets_prescriptions_via_advanced_search()
        {
            var client = _factory.CreateClient();
            var content = JsonContent.Create(new PrescriptionAdvancedFilterDto
            {
                Name = "Brufen",
                Status = TimeStatus.All
            });
            var response = await client.PostAsync("/docsearch/prescription/advanced", content);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Brufen", responseString);
        }
    }
}
