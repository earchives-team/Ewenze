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

            var requiredKeys = new List<string> { "first_name", "last_name", "phone_number" };

            var metaDict = await _userRepository.GetUserMetaDictionnaryAsync(request.Id, requiredKeys);


            // Ceci est un quick Fix 
            // Je dois utiliser autoMapper pour mapper les donner 


            var UserDto = new UserDto
            {
                Email = currentUser.Email,
                UserName = currentUser.LoginName,
                FirstName = metaDict.GetValueOrDefault("first_name"),
                LastName = metaDict.GetValueOrDefault("last_name"),
                phoneNumber = metaDict.GetValueOrDefault("phone_number"),
                NiceName = currentUser.NiceName,
                Id = currentUser.Id,
            }; 



            return UserDto; 
        }
    }
}
