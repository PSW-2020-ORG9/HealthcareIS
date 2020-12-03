using HospitalWebApp;
using Xunit;

namespace HospitalWebAppIntegrationTests
{
    public class PatientTests
    : IClassFixture<HospitalWebApplicationFactory<Startup>>
    {
        private readonly HospitalWebApplicationFactory<Startup> _factory;
        public PatientTests(HospitalWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async void Finds_patient()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/patient/find/1");

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
