﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Orders;
using Ordering.Domain.Events;
using Ordering.Domain.Orders;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController(
        OrderService service, IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<Ok<List<Order>>> GetListAsync()
        {
            List<Order> orders = await service.GetListAsync();

            await mediator.Publish(new OrderQueryDomainEvent(DateTime.Now));

            return TypedResults.Ok(orders);
        }

        [HttpPost]
        public async Task<Created> CreateAsync(Order order)
        {
            Order result = await service.CreateOrderAsync(order);
            return TypedResults.Created($"/orders/{result.Id}");
        }
    }
}