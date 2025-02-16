using AutoMapper;
using BCrypt.Net;
using Ewenze.Application.Exceptions;
using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Features.UserFeature.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator(this._userRepository); 

            var validationResult  = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Invalid User", validationResult);
            }

            // Convertir les données 
            var mappedUser = _mapper.Map<User>(request);

            mappedUser.UserStatus = 1;
            mappedUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            mappedUser.RegisteredDate = DateTime.Now;
            await _userRepository.CreateUser(mappedUser);

            return mappedUser.Id;
        }
    }
}
