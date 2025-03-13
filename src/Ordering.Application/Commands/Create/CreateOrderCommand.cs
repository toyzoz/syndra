using MediatR;
using Ordering.Application.Extensions;
using Ordering.Application.Models;

namespace Ordering.Application.Commands.Create;

public record CreateOrderCommand : IRequest<bool>
{
    public readonly List<OrderItemDto> Items;

    public CreateOrderCommand(List<BasketItem> basketItems,
        string userId,
        string userName,
        string city,
        string street,
        string state,
        string country,
        string zipCode,
        string cardNumber,
        string cardHolderName,
        DateTime cardExpiration,
        string cardSecurityNumber,
        int cardTypeId)
    {
        UserId = userId;
        UserName = userName;
        City = city;
        Street = street;
        State = state;
        Country = country;
        ZipCode = zipCode;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        CardExpiration = cardExpiration;
        CardSecurityNumber = cardSecurityNumber;
        CardTypeId = cardTypeId;

        Items = basketItems.Select(i => i.ToOrderItemDto()).ToList();
    }

    public string UserId { get; private set; }

    public string UserName { get; private set; }

    public string City { get; private set; }

    public string Street { get; private set; }

    public string State { get; private set; }

    public string Country { get; private set; }

    public string ZipCode { get; private set; }

    public string CardNumber { get; private set; }

    public string CardHolderName { get; private set; }

    public DateTime CardExpiration { get; private set; }

    public string CardSecurityNumber { get; private set; }

    public int CardTypeId { get; private set; }
}