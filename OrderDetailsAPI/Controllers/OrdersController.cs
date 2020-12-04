using DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersAPI.Client;
using System.Threading.Tasks;

namespace OrderDetailsAPI.Controllers
{
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersAPIClient ordersClient;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(OrdersAPIClient client, ILogger<OrdersController> logger)
        {
            this.ordersClient = client;
            this.logger = logger;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> SaveOrderAsync(int userId, [FromBody]OrderDto order)
        {
            logger.LogInformation($"{nameof(SaveOrderAsync)} called for {nameof(userId)}:{userId}");
            bool result = await ordersClient.SaveOrderAsync(userId, order);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{orderId}")]
        public async Task<ActionResult<bool>> DeleteOrderAsync(int userId, int orderId)
        {
            logger.LogInformation($"{nameof(DeleteOrderAsync)} called for {nameof(userId)}:{userId} {nameof(orderId)}:{orderId}");
            bool result = await ordersClient.DeleteOrderAsync(userId, orderId);
            return Ok(result);
        }
    }
}
