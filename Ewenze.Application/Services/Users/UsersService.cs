using Ewenze.Application.Services.Users.Exceptions;
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
            {
                throw new UsersException($"The User with id {userId} was not found") 
                {
                    Reason = UsersExceptionReason.EntityNotFound, 
                    InvalidProperty = "userId" 
                };
            }

            return UserConverter.Convert(currentUser);
        }

        public async Task<UserApplicationModel> GetByEmailAsync(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
                throw new UsersException($"email cannot be null") { Reason = UsersExceptionReason.InvalidProperty, InvalidProperty = "email" };

            var currentUser = await UserRepository.GetUserByEmailAsync(email);

            if (currentUser == null)
            {
                throw new UsersException($"The User with email {email} was not found") { Reason = UsersExceptionReason.EntityNotFound, InvalidProperty = "email" };
            }

            return UserConverter.Convert(currentUser);
        }

        public async Task<int> CreateAsync(UserApplicationModel user)
        {
            var validator = new CreateUserValidator(this.UserRepository);

            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
                throw new UsersException("Invalid Properties", validationResult);

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

            return convertedUser.Id; 
        }

    }
}
