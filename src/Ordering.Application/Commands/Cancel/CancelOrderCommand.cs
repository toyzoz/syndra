using MediatR;

namespace Ordering.Application.Commands.Cancel;

public record CancelOrderCommand(int OrderNumber) : IRequest<bool>;