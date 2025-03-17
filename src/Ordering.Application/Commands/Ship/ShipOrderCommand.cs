using MediatR;

namespace Ordering.Application.Commands.Ship;

public record ShipOrderCommand(int OrderNumber) : IRequest<bool>;