using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Features.UserFeature.Queries.GetUserById
{
    public record class GetUserByIdQuery(int id) : IRequest<User>;


    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetUserById(request.id);

            // Must Had Validation and throw error 
            

            return currentUser!; 
        }
    }
}
