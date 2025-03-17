using EventBus;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Extensions;

namespace Ordering.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling command {CommandName} ({@Command})",
            request.GetGenericTypeName(),
            request);
        TResponse? response = await next();
        logger.LogInformation("Command {CommandName} handled - response: {@Response}",
            request.GetGenericTypeName(),
            response);

        return response;
    }
}
