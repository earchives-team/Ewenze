using Ewenze.API.Models.UsersDto;
using Ewenze.Application.Services.Users;
using Ewenze.Application.Services.Users.Exceptions;
using Ewenze.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Ewenze.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize] // Seuls les utilisateurs authentifiés peuvent accéder
    public class UsersController : ControllerBase
    {
        private readonly IUsersService UsersService;
        private readonly Converters.UserConverter UserConverter;
        private readonly IOptions<ApiBehaviorOptions> apiBehaviorOptions;

        public UsersController(IUsersService usersService, Converters.UserConverter userConverter, IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            this.UsersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            this.UserConverter = userConverter ?? throw new ArgumentNullException(nameof(userConverter));
            this.apiBehaviorOptions = apiBehaviorOptions ?? throw new ArgumentNullException(nameof(apiBehaviorOptions));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserOutputDto>), 200)]
        public async Task<IActionResult> Get()
        {
            var serviceModels = await UsersService.GetAllAsync();
            var outputDtos = UserConverter.Convert(serviceModels);
            return Ok(outputDtos);
        }

        // GET: api/users/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {

            var serviceModel = await UsersService.GetById(id);
            var outputDtos = UserConverter.Convert(serviceModel);
            return Ok(outputDtos);
        
        }

        // GET: api/users/by-email?email=value
        [HttpGet]
        [ProducesResponseType(typeof(UserOutputDto), 200)]
        [Route("by-email")]
        public async Task<IActionResult> GetUserByEmail([Required][FromQuery]string email)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            var serviceModel = await UsersService.GetByEmailAsync(email);
            var outputDtos = UserConverter.Convert(serviceModel);
            return Ok(outputDtos); 
        }

        // POST: api/users
        [HttpPost]
        [AllowAnonymous] // Cette action est accessible sans authentification
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(UserInputDto userInputDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userConverted = UserConverter.Convert(userInputDto);
            var userId = await UsersService.CreateAsync(userConverted); 

            return CreatedAtAction(nameof(GetUserById), new { id = userId }, userConverted);
        }
    }
}
