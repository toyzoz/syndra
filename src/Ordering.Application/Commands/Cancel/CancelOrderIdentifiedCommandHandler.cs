using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands.Identified;
using Ordering.Infrastructure.Idempotency;

namespace Ordering.Application.Commands.Cancel;

public class CancelOrderIdentifiedCommandHandler(
    IRequestManager requestManager,
    IMediator mediator,
    ILogger<IdentifiedCommandHandler<CancelOrderCommand, bool>> logger)
    : IdentifiedCommandHandler<CancelOrderCommand, bool>(requestManager, mediator, logger)
{
    protected override bool CreateResultForDuplicateRequest()
    {
        return true;
    }
}