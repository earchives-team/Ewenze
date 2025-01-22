using Ewenze.Application.Features.UserFeature.Queries.GetUsers;
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
        public async Task<IActionResult> GetUsers()
        {
            return Ok( await mediator.Send(new GetUsersQuery()));
        }
    }
}
