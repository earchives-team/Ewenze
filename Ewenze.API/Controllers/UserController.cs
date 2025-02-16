using Ewenze.Application.Features.UserFeature.Commands.CreateUser;
using Ewenze.Application.Features.UserFeature.Dto;
using Ewenze.Application.Features.UserFeature.Queries.GetUserByEmail;
using Ewenze.Application.Features.UserFeature.Queries.GetUserById;
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
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok( await _mediator.Send(new GetUsersQuery()));
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDto),200)]
        [Route("GetByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            return Ok(await _mediator.Send(new GetUserByEmailQuery(email)));
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(id))); 
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(CreateUserCommand userCommand)
        {
            var response = await _mediator.Send(userCommand);

            return CreatedAtAction(nameof(GetUserById), new { id = response }, response);
        }
    }
}
