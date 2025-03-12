using Catalog.API.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.FunctionalTests
{
    public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
    {
        protected readonly HttpClient Client;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            Client = factory.CreateClient();
        }
    }
}