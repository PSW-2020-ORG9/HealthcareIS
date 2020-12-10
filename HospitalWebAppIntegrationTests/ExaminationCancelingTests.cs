using HospitalWebApp;
using System.Net;
using Xunit;

namespace HospitalWebAppIntegrationTests
{
    public class ExaminationCancelingTests
    : IClassFixture<HospitalWebApplicationFactory<Startup>>
    {
        private readonly HospitalWebApplicationFactory<Startup> _factory;
        public ExaminationCancelingTests(HospitalWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Cancel_examination_success()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/examination/cancel/2");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void Cancel_examination_fail()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/examination/cancel/3");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
