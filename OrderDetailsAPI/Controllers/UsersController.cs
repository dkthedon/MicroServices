using DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersAPI.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersAPI.Client;

namespace OrderDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersAPIClient usersClient;
        private readonly OrdersAPIClient ordersClient;
        private readonly ILogger<UsersController> logger;

        public UsersController(UsersAPIClient usersClient, OrdersAPIClient ordersClient, ILogger<UsersController> logger)
        {
            this.usersClient = usersClient;
            this.ordersClient = ordersClient;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync()
        {
            logger.LogInformation($"{nameof(GetUsersAsync)} called");
            var users = await usersClient.GetUsersAsync().ConfigureAwait(false);
            return Ok(users);
        }

        [HttpGet]
        [Route("names")]
        public async Task<ActionResult<IEnumerable<UserNameDto>>> GetUserNamesAsync()
        {
            logger.LogInformation($"{nameof(GetUserNamesAsync)} called");
            var usernames = await usersClient.GetUserNamesAsync().ConfigureAwait(false);
            return Ok(usernames);
        }

        [HttpPut]
        public async Task<IActionResult> SaveUserAsync(UserDto user)
        {
            logger.LogInformation($"{nameof(SaveUserAsync)} called");
            await usersClient.SaveUserAsync(user).ConfigureAwait(false);
            return Ok();
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            logger.LogInformation($"{nameof(DeleteUserAsync)} called for {nameof(userId)}:{userId}");
            await ordersClient.DeleteOrdersAsync(userId);
            await usersClient.DeleteUserAsync(userId);
            return Ok();
        }
    }
}
