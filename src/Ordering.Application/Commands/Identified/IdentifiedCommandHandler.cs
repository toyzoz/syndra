using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands.Cancel;
using Ordering.Application.Commands.Create;
using Ordering.Application.Commands.Ship;
using Ordering.Application.Extensions;

namespace Ordering.Application.Commands.Identified;

public abstract class IdentifiedCommandHandler<T, R>(
    IRequestManager requestManager,
    IMediator mediator,
    ILogger<IdentifiedCommandHandler<T, R>> logger)
    : IRequestHandler<IdentifiedCommand<T, R>, R>
    where T : IRequest<R>
{
    public async Task<R> Handle(
        IdentifiedCommand<T, R> request,
        CancellationToken cancellationToken)
    {
        if (await requestManager.ExistAsync(request.Id)) return CreateResultForDuplicateRequest();

        await requestManager.CreateRequestForCommandAsync<T>(request.Id);

        try
        {
            var command = request.Command;
            var commandName = command.GetGenericTypeName();
            string idProperty;
            string commandId;

            switch (request.Command)
            {
                case CreateOrderCommand createOrderCommand:
                    idProperty = nameof(createOrderCommand.UserId);
                    commandId = createOrderCommand.UserId;
                    break;

                case CancelOrderCommand cancelOrderCommand:
                    idProperty = nameof(cancelOrderCommand.OrderNumber);
                    commandId = $"{cancelOrderCommand.OrderNumber}";
                    break;

                case ShipOrderCommand shipOrderCommand:
                    idProperty = nameof(shipOrderCommand.OrderNumber);
                    commandId = $"{shipOrderCommand.OrderNumber}";
                    break;

                default:
                    idProperty = "Id?";
                    commandId = "n/a";
                    break;
            }

            logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                commandName,
                idProperty,
                commandId,
                command);

            var result = await mediator.Send(command, cancellationToken);

            logger.LogInformation(
                "Command result: {@Result} - {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                result,
                commandName,
                idProperty,
                commandId,
                command);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected abstract R CreateResultForDuplicateRequest();
}