using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands.Identified;

namespace Ordering.Application.Commands.Ship;

public class ShipOrderIdentifiedCommandHandler(
    IRequestManager requestManager,
    IMediator mediator,
    ILogger<IdentifiedCommandHandler<ShipOrderCommand, bool>> logger)
    : IdentifiedCommandHandler<ShipOrderCommand, bool>(requestManager, mediator, logger)
{
    protected override bool CreateResultForDuplicateRequest() => true;
}
