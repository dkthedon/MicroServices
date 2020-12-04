using DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersAPI.Mappers;
using UsersAPI.Repository;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly ILogger<UsersController> logger;
        private readonly ITracer tracer;

        public UsersController(UnitOfWork unitOfWork, ITracer tracer, ILogger<UsersController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.tracer = tracer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAsync()
        {
            logger.LogInformation($"{nameof(GetAsync)} called");
            var users = await unitOfWork.UsersRepository.GetAsync().ConfigureAwait(false);
            return Ok(users.Select(x => x.Map()));
        }

        [HttpGet]
        [Route("names")]
        public async Task<ActionResult<IEnumerable<UserNameDto>>> GetNamesAsync()
        {
            using (IScope scope = tracer.BuildSpan(nameof(GetNamesAsync)).StartActive(finishSpanOnDispose: true))
            {
                scope.Span.Log($"{nameof(GetNamesAsync)} called");
                var users = await unitOfWork.UsersRepository.GetAsync().ConfigureAwait(false);
                return Ok(users.Select(x => x.MapName()));
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(int userId)
        {
            logger.LogInformation($"{nameof(GetByIdAsync)} called for {nameof(userId)}:{userId}");
            var user = await unitOfWork.UsersRepository.GetByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
                return NotFound();
            return Ok(user.Map());
        }   

        [HttpPut]
        public async Task<IActionResult> SaveAsync([FromBody]UserDto userDto)
        {
            logger.LogInformation($"{nameof(SaveAsync)} called");
            var user = userDto.Map();
            unitOfWork.UsersRepository.Insert(user);
            await unitOfWork.SaveAsync().ConfigureAwait(false);
            return Ok();
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteAsync(int userId)
        {
            logger.LogInformation($"{nameof(DeleteAsync)} called for {nameof(userId)}:{userId}");
            unitOfWork.UsersRepository.Delete(userId);
            await unitOfWork.SaveAsync().ConfigureAwait(false);
            return NoContent();
        }
    }
}
