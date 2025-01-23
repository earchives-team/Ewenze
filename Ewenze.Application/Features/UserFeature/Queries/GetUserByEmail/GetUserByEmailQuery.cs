using Ewenze.Domain.Entities;
using Ewenze.Domain.Exceptions;
using Ewenze.Domain.Repositories;
using MediatR;

namespace Ewenze.Application.Features.UserFeature.Queries.GetUserByEmail
{
    public record class GetUserByEmailQuery(string email) : IRequest<User>;

    internal class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
    {
        private IUserRepository UserRepository;

        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await UserRepository.GetUserByEmailAsync(request.email);

            if(user == null)
            {
                throw new NotFoundException(nameof(user), request.email);
            }

            return user;
        }
    }
}
