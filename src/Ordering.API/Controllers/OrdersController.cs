using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ordering.Doamin.Orders;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController(IOrderRepository repository,
        ILogger<OrdersController> logger) : ControllerBase
    {




        [HttpGet]
        public async Task<Ok<IEnumerable<Order>>> GetListAsync()
        {
            var result = await repository.GetListAsync();

            return TypedResults.Ok(result);
        }
        [HttpPost]
        public async Task<Ok<CreatedAtRoute<Order>>> GetListAsync(Order order)
        {
            var result = await repository.CreateAsync(order);

            throw new NotImplementedException();
        }
    }
}
