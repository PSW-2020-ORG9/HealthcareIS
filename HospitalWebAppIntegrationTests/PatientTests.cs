using HospitalWebApp;
using Xunit;

namespace HospitalWebAppIntegrationTests
{
    public class PatientTests
    : IClassFixture<HospitalWebApplicationFactory<TestStartup>>
    {
        private readonly HospitalWebApplicationFactory<TestStartup> _factory;
        public PatientTests(HospitalWebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async void Finds_patient()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/patient/find/1");
            var responseData = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1,", responseData);
        }

        [Fact]
        public async void Gets_patient_examinations()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/examination/patient/1");
            var responseData = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"patientId\":1", responseData);
        }
    }
}
