using AutoMapper;
using Ewenze.Application.Features.UserFeature.Dto;
using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using MediatR;

namespace Ewenze.Application.Features.UserFeature.Queries.GetUsers
{
    public record class GetUsersQuery : IRequest<IEnumerable<UserDto>>;

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository UserRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            UserRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var allUsers = await UserRepository.GetUsersAsync();

            return _mapper.Map<IEnumerable<UserDto>>(allUsers);
        }
    }
}
