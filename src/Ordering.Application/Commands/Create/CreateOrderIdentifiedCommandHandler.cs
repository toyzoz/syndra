using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands.Identified;

namespace Ordering.Application.Commands.Create;

public class CreateOrderIdentifiedCommandHandler(
    IRequestManager requestManager,
    IMediator mediator,
    ILogger<IdentifiedCommandHandler<CreateOrderCommand, bool>> logger)
    : IdentifiedCommandHandler<CreateOrderCommand, bool>(requestManager, mediator, logger)
{
    protected override bool CreateResultForDuplicateRequest() => true;
}
