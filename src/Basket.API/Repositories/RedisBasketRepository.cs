using System.Text.Json;
using Basket.API.Models;
using StackExchange.Redis;

namespace Basket.API.Repositories;

public class RedisBasketRepository(
    IConnectionMultiplexer multiplexer,
    ILogger<RedisBasketRepository> logger) : IBasketRepository
{
    private static readonly RedisKey BasketKeyPrefix = "basket"u8.ToArray();
    private IDatabase Database => multiplexer.GetDatabase();

    public async Task<CustomerBasket> GetBasketAsync(string customerId)
    {
        using Lease<byte>? data = await Database.StringGetLeaseAsync(GetBasketKey(customerId));
        return data is null ? new CustomerBasket() : JsonSerializer.Deserialize<CustomerBasket>(data.Span)!;
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        byte[]? json = JsonSerializer.SerializeToUtf8Bytes(basket);
        bool created = await Database.StringSetAsync(GetBasketKey(basket.BuyerId), json);
        if (!created)
        {
            logger.LogInformation("Problem occurred persisting the item.");
            await GetBasketAsync(basket.BuyerId);
        }

        logger.LogInformation("Basket item persisted successfully.");
        return await GetBasketAsync(basket.BuyerId);
    }

    public async Task<bool> DeleteBasketAsync(string id) => await Database.KeyDeleteAsync(GetBasketKey(id));

    private static RedisKey GetBasketKey(string userId) => BasketKeyPrefix.Append(userId);
}
