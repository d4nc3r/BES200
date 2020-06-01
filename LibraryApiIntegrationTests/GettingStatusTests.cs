using LibraryApi.Controllers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class GettingStatusTests : IClassFixture<WebTestFixture>
    {
        private HttpClient Client;

        public GettingStatusTests(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetTheStatus()
        {
            var response = await Client.GetAsync("/status");
            Assert.True(response.IsSuccessStatusCode);

            var content = await response.Content.ReadAsAsync<GetStatusResponse>();

            Assert.Equal("Everything is golden!", content.Message);
            Assert.Equal("Joe Schmidtly", content.CheckedBy);
            Assert.Equal(new DateTime(1979, 01, 22, 16, 20, 00), content.WhenLastChecked);
        }
    }
}
