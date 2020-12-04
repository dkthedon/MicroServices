using DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace OrdersAPI.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderManager orderManager;
        private readonly ILogger<OrdersController> logger;
        public OrdersController(OrderManager orderManager, ILogger<OrdersController> logger)
        {
            this.orderManager = orderManager;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetOrders(int userId)
        {
            logger.LogInformation($"{nameof(GetOrders)} called for {nameof(userId)}:{userId}");
            var orders = orderManager.GetOrders(userId);
            return Ok(orders);
        }

        [HttpGet]
        [Route("{orderId:int}")]
        public ActionResult<OrderDto> GetOrder(int userId, int orderId)
        {
            logger.LogInformation($"{nameof(GetOrder)} called for {nameof(orderId)}:{orderId}");
            var order = orderManager.GetOrder(userId, orderId);
            if (order == null)
            {
                logger.LogWarning($"{nameof(GetOrder)} called for invalid {nameof(orderId)}:{orderId}");
                return BadRequest($"Invalid {nameof(orderId)}: {orderId}");
            }

            return Ok(order);
        }

        [HttpPut]
        public ActionResult<bool> SaveOrder(int userId, [FromBody] OrderDto order)
        {
            logger.LogInformation($"{nameof(SaveOrder)} called");
            var sucess = orderManager.SaveOrder(userId, order);
            return Ok(sucess);
        }

        [HttpDelete]
        [Route("{orderId:int}")]
        public ActionResult<bool> DeleteOrder(int userId, int orderId)
        {
            logger.LogInformation($"{nameof(DeleteOrder)} called for {nameof(orderId)}:{orderId}");
            var success = orderManager.DeleteOrder(userId, orderId);
            return Ok(success);
        }

        [HttpDelete]
        public ActionResult<bool> DeleteOrders(int userId)
        {
            logger.LogInformation($"{nameof(DeleteOrders)} called for {nameof(userId)}:{userId}");
            var success = orderManager.DeleteOrders(userId);
            return Ok(success);
        }
    }

}
