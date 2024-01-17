using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider) 
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetOrders(int CustomerId) 
        {
            var result = await ordersProvider.GetOrdersAsync(CustomerId);
            if(!result.IsSuccess)  { return NotFound(); }
            return Ok(result.Item2);

        }

        [HttpGet("{CustomerId}/{OrderId}")]
        public async Task<IActionResult> GetOrder(int CustomerId, int OrderId) 
        {
           var result = await ordersProvider.GetOrderAsync(CustomerId, OrderId);
            if (!result.IsSuccess) { return NotFound(); }
            return Ok(result.Item2);
        }
    }
}
