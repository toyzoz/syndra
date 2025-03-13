using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands.Cancel;
using Ordering.Application.Commands.Create;
using Ordering.Application.Commands.Ship;
using Ordering.Application.Orders;
using Ordering.Domain.Orders;

namespace Ordering.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController(
    OrderService service,
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<Ok<List<Order>>> GetListByUserAsync()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:int}")]
    public async Task<Ok<Order>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }


    [HttpPost]
    public async Task<Created> CreateAsync(CreateOrderCommand command)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<Created> CreateDraftAsync(Order order)
    {
        throw new NotImplementedException();
    }

    [HttpPut($"{{id:int}}")]
    public async Task CancelAsync(CancelOrderCommand command)
    {
        throw new NotImplementedException();
    }

    [HttpPut($"{{id:int}}")]
    public async Task ShipAsync(ShipOrderCommand command)
    {
        throw new NotImplementedException();
    }
}