using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using MediatR;

namespace Ewenze.Application.Features.UserFeature.Queries.GetUsers
{
    public record class GetUsersQuery : IRequest<IEnumerable<User>>;

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository UserRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var allUsers = await UserRepository.GetUsersAsync();

            return allUsers;
        }
    }
}
