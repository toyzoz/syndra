using System.Net;

namespace Catalog.API.FunctionalTests;

public class BrandsControllerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetListAsync()
    {
        HttpResponseMessage? response = await Client.GetAsync("/Brands");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
