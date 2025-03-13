using Basket.API.Services;
using Grpc.Net.Client;

namespace Basket.API.FunctionalTests;

public class BasketServiceTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)

{
    [Fact]
    public void Test1()
    {
        // new Basket.BasketClient(
        //     GrpcChannel.ForAddress("http://localhost:5000"));
    }
}