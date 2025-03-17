


using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands.Cancel;
using Ordering.Application.Commands.Create;
using Ordering.Application.Commands.CreateDraft;
using Ordering.Application.Commands.Identified;
using Ordering.Application.Commands.Ship;
using Ordering.Application.Orders;
using Ordering.Application.Queries;
using Ordering.Application.Queries.ViewModels;
using Ordering.Domain.AggregateModels.Orders;

namespace Ordering.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController(
    OrderService service,
    IMediator sender,
    IOrderQuery orderQuery,
    ILogger<OrdersController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<Ok<IEnumerable<Order>>> GetListByUserAsync()
    {
        const string user = "bob";

        IEnumerable<Order>? result = await orderQuery.GetOrderByUserAsync(user);
        return TypedResults.Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<Ok<Order>> GetByIdAsync(int id)
    {
        Order? result = await orderQuery.GetOrderByIdAsync(id);
        return TypedResults.Ok(result);
    }

    [HttpGet("card-type")]
    public async Task<Ok<List<CardTypeOutput>>> GetCardTypesAsync()
    {
        List<CardTypeOutput>? result = await orderQuery.GetCardTypesAsync();
        return TypedResults.Ok(result);
    }


    [HttpPost]
    public async Task<Ok> CreateAsync(CreateOrderCommand request)
    {
        // todo: requestId
        Guid requestId = Guid.NewGuid();
        IdentifiedCommand<CreateOrderCommand, bool> identifiedCommand = new(requestId, request);
        bool result = await sender.Send(identifiedCommand);

        if (result)
        {
            logger.LogInformation("CreateOrderCommand succeeded - RequestId: {RequestId}", requestId);
        }
        else
        {
            logger.LogWarning("CreateOrderCommand failed - RequestId: {RequestId}", requestId);
        }

        return TypedResults.Ok();
    }

    [HttpPost("draft")]
    public async Task<OrderDraftDto> CreateDraftAsync(CreateOrderDraftCommand request) => await sender.Send(request);

    [HttpPut("cancel")]
    public async Task<Ok<bool>> CancelAsync(CancelOrderCommand command)
    {
        IdentifiedCommand<CancelOrderCommand, bool> request = new(Guid.NewGuid(), command);
        bool result = await sender.Send(request);
        return TypedResults.Ok(result);
    }

    [HttpPut("ship")]
    public async Task<Ok<bool>> ShipAsync(ShipOrderCommand command)
    {
        IdentifiedCommand<ShipOrderCommand, bool> request = new(Guid.NewGuid(), command);
        bool result = await sender.Send(request);
        return TypedResults.Ok(result);
    }
}
