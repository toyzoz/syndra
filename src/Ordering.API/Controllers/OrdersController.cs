using MediatR;
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
        OrderService service,
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<Ok<List<Order>>> GetListAsync()
        {
            var orders = await service.GetListAsync();

            await mediator.Publish(new OrderQueryDomainEvent(DateTime.Now));

            return TypedResults.Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<Ok<Order>> GetAsync(int id)
        {
            var order = await service.GetAsync(id);
            
            return TypedResults.Ok(order);
        }


        [HttpPost]
        public async Task<Created> CreateAsync(Order order)
        {
            var result = await service.CreateOrderAsync(order);
            return TypedResults.Created($"/orders/{result.Id}");
        }
    }
    
    // 创建订单输入
    public class CreateOrder
    {
        // 买家
        
            
    }
}