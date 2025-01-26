using AutoMapper;
using Ewenze.Application.Features.UserFeature.Dto;
using Ewenze.Domain.Entities;
using Ewenze.Domain.Exceptions;
using Ewenze.Domain.Repositories;
using MediatR;

namespace Ewenze.Application.Features.UserFeature.Queries.GetUserById
{
    public record class GetUserByIdQuery(int Id) : IRequest<UserDto>;


    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetUserById(request.Id);
            
            if (currentUser == null)
            {
                throw new NotFoundException(nameof(currentUser), request.Id);
            }

            return _mapper.Map<UserDto>(currentUser); 
        }
    }
}
