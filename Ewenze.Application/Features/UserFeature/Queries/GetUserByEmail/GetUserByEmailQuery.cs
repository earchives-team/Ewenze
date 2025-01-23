using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var singleUser = await UserRepository.GetUserByEmailAsync(request.email);

            // Must implement The exceptions when No User  etc
            //For now assume the user exist 

            return singleUser!;
        }
    }
}
