using Basket.API.Services;
using Grpc.Net.Client;

namespace Basket.API.FunctionalTests;

public class BasketServiceTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)

{
    [Fact]
    public void Test1()
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:5001");

        var client = new Basket.BasketClient(channel);

    }
}