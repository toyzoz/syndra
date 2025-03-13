using MediatR;
using Ordering.Application.Models;

namespace Ordering.Application.Commands.CreateDraft;

public record CreateOrderDraftCommand(
    string BuyerId,
    IEnumerable<BasketItem> Items)
    : IRequest<OrderDraftDto>;