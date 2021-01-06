using Xunit;

namespace User.API.IntegrationTests
{
    public class LocaleTests
        : IClassFixture<UserApiFactory<TestStartup>>
    {
        private readonly UserApiFactory<TestStartup> _factory;
        public LocaleTests(UserApiFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Gets_city_by_country()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("user/city/by-country/1");
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Novi Sad", responseString);
        }

        [Fact]
        public async void Gets_countries()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("user/country");
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Srbija", responseString);
        }
    }
}
