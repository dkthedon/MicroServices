using DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersAPI.Client;
using System.Threading.Tasks;
using UsersAPI.Client;

namespace OrderDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrdersAPIClient ordersClient;
        private readonly UsersAPIClient usersClient;
        private ILogger<OrderDetailsController> logger;

        public OrderDetailsController(OrdersAPIClient ordersClient, UsersAPIClient usersClient, ILogger<OrderDetailsController> logger)
        {
            this.ordersClient = ordersClient;
            this.usersClient = usersClient;
            this.logger = logger;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrdersAsync(int userId)
        {
            var user = await usersClient.GetUserAsync(userId).ConfigureAwait(false);
            if (user == null)
                return BadRequest($"User with id:{userId} doesn't exist");

            var orders = await ordersClient.GetOrdersAsync(userId).ConfigureAwait(false);
            var orderDetails = new OrderDetailsDto
            {
                User = user,
                Orders = orders
            };

            return Ok(orderDetails);
        }
    }
}
