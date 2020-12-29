using System;
using Xunit;

namespace Feedback.API.IntegrationTests
{
    public class FeedbackTests
        : IClassFixture<FeedbackApiFactory<TestStartup>>
    {
        private readonly FeedbackApiFactory<TestStartup> _factory;
        public FeedbackTests(FeedbackApiFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Gets_published_feedbacks()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/feedback/published");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"isPublished\":true", responseString);
        }
    }
}
