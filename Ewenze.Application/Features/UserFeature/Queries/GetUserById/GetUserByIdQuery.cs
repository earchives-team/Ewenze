using Ewenze.Domain.Entities;
using Ewenze.Domain.Exceptions;
using Ewenze.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Features.UserFeature.Queries.GetUserById
{
    public record class GetUserByIdQuery(int Id) : IRequest<User>;


    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetUserById(request.Id);
            
            if (currentUser == null)
            {
                throw new NotFoundException(nameof(currentUser), request.Id);
            }

            return currentUser!; 
        }
    }
}
