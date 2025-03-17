using System.Net;

namespace Catalog.API.FunctionalTests;

public class BrandsControllerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetListAsync()
    {
        var response = await Client.GetAsync("/brands");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}