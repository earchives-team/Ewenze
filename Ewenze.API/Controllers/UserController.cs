using Ewenze.Application.Features.UserFeature.Queries.GetUserByEmail;
using Ewenze.Application.Features.UserFeature.Queries.GetUsers;
using Ewenze.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ewenze.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok( await mediator.Send(new GetUsersQuery()));
        }

        [HttpGet]
        [ProducesResponseType(typeof(User),200)]
        [Route("GetByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            return Ok(await mediator.Send(new GetUserByEmailQuery(email)));
        }
    }
}
