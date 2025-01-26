using AutoMapper;
using Ewenze.Application.Features.UserFeature.Dto;
using Ewenze.Domain.Entities;
using Ewenze.Domain.Exceptions;
using Ewenze.Domain.Repositories;
using MediatR;

namespace Ewenze.Application.Features.UserFeature.Queries.GetUserByEmail
{
    public record class GetUserByEmailQuery(string email) : IRequest<UserDto>;

    internal class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private IUserRepository UserRepository;
        private IMapper _mapper;
        public GetUserByEmailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            UserRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await UserRepository.GetUserByEmailAsync(request.email);

            if(user == null)
            {
                throw new NotFoundException(nameof(user), request.email);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
