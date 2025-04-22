using Ewenze.Application.Features.UserFeature.Commands.CreateUser;
using Ewenze.Application.Features.UserFeature.Dto;
using Ewenze.Application.Features.UserFeature.Queries.GetUserByEmail;
using Ewenze.Application.Features.UserFeature.Queries.GetUserById;
using Ewenze.Application.Features.UserFeature.Queries.GetUsers;
using Ewenze.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ewenze.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize] // Seuls les utilisateurs authentifiés peuvent accéder
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok( await _mediator.Send(new GetUsersQuery()));
        }

        // GET: api/users/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(id)));
        }

        // GET: api/users/by-email?email=value
        [HttpGet]
        [ProducesResponseType(typeof(UserDto), 200)]
        [Route("by-email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery]string email)
        {
            return Ok(await _mediator.Send(new GetUserByEmailQuery(email)));
        }

        // POST: api/users
        [HttpPost]
        [AllowAnonymous] // Cette action est accessible sans authentification
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(CreateUserCommand userCommand)
        {
            var response = await _mediator.Send(userCommand);

            return CreatedAtAction(nameof(GetUserById), new { id = response }, response);
        }
    }
}
