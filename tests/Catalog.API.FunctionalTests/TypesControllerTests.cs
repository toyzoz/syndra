using System.Net;

namespace Catalog.API.FunctionalTests
{
    public class TypesControllerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public async Task GetListAsync()
        {
            var response = await Client.GetAsync("/Types");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}