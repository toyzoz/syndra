using Basket.API.Extensions;
using Basket.API.Models;
using Basket.API.Repositories;
using Grpc.Core;
using System.Diagnostics.CodeAnalysis;

namespace Basket.API.Services
{
    public class BasketService(
        IBasketRepository repository,
        ILogger<BasketService> logger,
        IUserIdentityService userIdentityService)
        : Basket.BasketBase
    {
        public override async Task<CustomerBasketResponse> GetBasket(GetBasketRequest request,
            ServerCallContext context)
        {
            var userId = userIdentityService.GetUserIdentity();
            if (userId == null)
            {
                return new CustomerBasketResponse();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("Begin GetBasketById call from method {Method} for basket id {Id}", context.Method,
                    userId);
            }


            var customerBasket = await repository.GetBasketAsync(userId);

            return MapToCustomerBasketResponse(customerBasket);
        }

        private static CustomerBasketResponse MapToCustomerBasketResponse(CustomerBasket customerBasket)
        {
            var basketResponse = new CustomerBasketResponse();

            foreach (var item in customerBasket.Items)
            {
                basketResponse.Items.Add(new BaskItem { ProductId = item.ProductId, Quantity = item.Quantity });
            }

            return basketResponse;
        }

        public override async Task<DeleteBasketResponse> DeleteBasket(DeleteBasketRequest request,
            ServerCallContext context)
        {
            var userId = userIdentityService.GetUserIdentity();
            if (userId is null)
            {
                ThrowNotAuthenticated();
                // return new DeleteBasketResponse();
            }

            await repository.DeleteBasketAsync(userId);
            return new DeleteBasketResponse();
        }

        public override async Task<CustomerBasketResponse> UpdateBasket(UpdateBasketRequest request,
            ServerCallContext context)
        {
            var userId = userIdentityService.GetUserIdentity();
            if (userId == null)
            {
                ThrowNotAuthenticated();
            }

            var customerBasket = MapToCustomerBasket(userId, request);
            var updatedBasket = await repository.UpdateBasketAsync(customerBasket);
            return MapToCustomerBasketResponse(updatedBasket);
        }

        private static CustomerBasket MapToCustomerBasket(string userIdentity, UpdateBasketRequest request)
        {
            return new CustomerBasket
            {
                BuyerId = userIdentity,
                Items = request.Items.Select(x => new BasketItem { ProductId = x.ProductId, Quantity = x.Quantity })
                    .ToList()
            };
        }

        [DoesNotReturn]
        private static void ThrowNotAuthenticated()
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "User not authenticated"));
        }

        [DoesNotReturn]
        private static void ThrowBasketDoesNotExist(string userId)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Basket with buyer id {userId} does not exist"));
        }
    }
}