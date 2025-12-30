using Ewenze.Application.Exceptions;
using Ewenze.Application.Services.Users.Models;
using Ewenze.Domain.Repositories;

namespace Ewenze.Application.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository UserRepository;
        private readonly IUserConverter UserConverter;

        public UsersService(IUserRepository userRepository, IUserConverter userConverter)
        {
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            UserConverter = userConverter ?? throw new ArgumentNullException(nameof(userConverter));
        }

        public async Task<IList<UserApplicationModel>> GetAllAsync()
        {
            var allUsers = await UserRepository.GetUsersAsync();

           return UserConverter.Convert(allUsers);
        }

        public async Task<UserApplicationModel> GetById(int userId)
        {
            var currentUser = await UserRepository.GetUserByIdAsync(userId);

            if (currentUser == null)
                throw new NotFoundException(nameof(userId), userId);
            
            return UserConverter.Convert(currentUser);
        }

        public async Task<UserApplicationModel> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new BadRequestException("Email cannot be null");

            var currentUser = await UserRepository.GetUserByEmailAsync(email);

            if (currentUser == null)
            {
                throw new NotFoundException(nameof(email), email);
            }

            return UserConverter.Convert(currentUser);
        }

        public async Task<UserApplicationModel> CreateAsync(UserApplicationModel user)
        {
            var validator = new CreateUserValidator(this.UserRepository);

            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
                throw new BadRequestException("Invalid User Properties", validationResult);

            var convertedUser = new Domain.Entities.UserV2
            {
                Email = user.Email,
                Name = user.Name,
                Phone = user.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password), 
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                Birthday = user.BirthDate.Date,

            };

            var newUser = await UserRepository.CreateUserAsync(convertedUser); 

            if(newUser == null)
                throw new Exception();

            return UserConverter.Convert(newUser); 
        }

    }
}
