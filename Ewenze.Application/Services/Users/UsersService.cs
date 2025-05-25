using Ewenze.Application.Services.Users.Exceptions;
using Ewenze.Application.Services.Users.Models;
using Ewenze.Domain.Repositories;

namespace Ewenze.Application.Services.Users
{
    public class UsersService : IUsersService
    {
        private static readonly List<string> RequiredKeys = new List<string>()
        {
            "first_name", "last_name", "phone_number"
        };


        private readonly IUserRepository UserRepository;
        private readonly IUserConverter UserConverter;
        private readonly IUserMetaDataRepository UserMetaDataRepository;

        public UsersService(IUserRepository userRepository, IUserConverter userConverter, IUserMetaDataRepository userMetaDataRepository = null)
        {
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            UserConverter = userConverter ?? throw new ArgumentNullException(nameof(userConverter));
            UserMetaDataRepository = userMetaDataRepository ?? throw new ArgumentNullException(nameof(userMetaDataRepository));
        }

        public async Task<IList<User>> GetAllAsync()
        {
            var allUsers = await UserRepository.GetUsersAsync();
            var allUsersMetaData = (await UserMetaDataRepository.GetByMetaKeysAsync(RequiredKeys)).ToList();


            var metaByUserId = allUsersMetaData
                .GroupBy(x => x.UserId)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToDictionary(m => m.MetaKey, m => m.MetaValue)
                );


            var users = allUsers.Select(user =>
            {
                metaByUserId.TryGetValue(user.Id, out var meta);

                return UserConverter.Convert(
                    user,
                    meta?.GetValueOrDefault("first_name"),
                    meta?.GetValueOrDefault("last_name"),
                    meta?.GetValueOrDefault("phone_number")
                );
            }).Where(u => u != null).ToList();

            return users; 
        }

        public async Task<User> GetById(int userId)
        {
            var currentUser = await UserRepository.GetUserById(userId);

            if (currentUser == null)
            {
                throw new UsersException($"The User with id {userId} was not found") 
                {
                    Reason = UsersExceptionReason.EntityNotFound, 
                    InvalidProperty = "userId" 
                };
            }

            var userMetaData = (await UserMetaDataRepository.GetByUserIdBAndByMetaKeysAsync(userId, RequiredKeys)).ToList();
            var meta = userMetaData.ToDictionary(m => m.MetaKey, m => m.MetaValue);

            return UserConverter.Convert(currentUser, meta?.GetValueOrDefault("first_name"), meta?.GetValueOrDefault("last_name"), meta?.GetValueOrDefault("phone_number"));
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
                throw new UsersException($"email cannot be null") { Reason = UsersExceptionReason.InvalidProperty, InvalidProperty = "email" };

            var currentUser = await UserRepository.GetUserByEmailAsync(email);

            if (currentUser == null)
            {
                throw new UsersException($"The User with email {email} was not found") { Reason = UsersExceptionReason.EntityNotFound, InvalidProperty = "email" };
            }

            var userMetaData = (await UserMetaDataRepository.GetByUserIdBAndByMetaKeysAsync(currentUser.Id, RequiredKeys)).ToList();
            var meta = userMetaData.ToDictionary(m => m.MetaKey, m => m.MetaValue);

            return UserConverter.Convert(currentUser, meta?.GetValueOrDefault("first_name"), meta?.GetValueOrDefault("last_name"), meta?.GetValueOrDefault("phone_number"));
        }

        public async Task<User> GetByUserNameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new UsersException($"username cannot be null") { Reason = UsersExceptionReason.InvalidProperty, InvalidProperty = "username" };

            var currentUser = await UserRepository.GetUserByEmailAsync(username);

            if (currentUser == null)
            {
                throw new UsersException($"The User with username {username} was not found") { Reason = UsersExceptionReason.EntityNotFound, InvalidProperty = "username" };
            }

            var userMetaData = (await UserMetaDataRepository.GetByUserIdBAndByMetaKeysAsync(currentUser.Id, RequiredKeys)).ToList();
            var meta = userMetaData.ToDictionary(m => m.MetaKey, m => m.MetaValue);

            return UserConverter.Convert(currentUser, meta?.GetValueOrDefault("first_name"), meta?.GetValueOrDefault("last_name"), meta?.GetValueOrDefault("phone_number"));
        }

        public async Task<int> Create(User user)
        {
            var validator = new CreateUserValidator(this.UserRepository);

            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
                throw new UsersException("Invalid Properties", validationResult);

            var convertedUser = new Domain.Entities.User
            {
                Email = user.Email,
                NiceName = user.NiceName,
                DisplayName = user.UserName,
                LoginName = user.UserName,
                UserStatus = 1,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password), 
                RegisteredDate = DateTime.Now,
            };

            var newUser = await UserRepository.CreateUser(convertedUser); 

            if(newUser != null)
            {
                var metaInfoList = new List<Domain.Entities.UserMeta>()
                {
                    new Domain.Entities.UserMeta
                    {
                         UserId = newUser.Id,
                         MetaKey = "first_name",
                         MetaValue = user.FirstName
                    },
                    new Domain.Entities.UserMeta
                    {
                        UserId = newUser.Id,
                        MetaKey = "last_name",
                        MetaValue = user.LastName
                    },
                    new Domain.Entities.UserMeta
                    {
                        UserId = newUser.Id,
                        MetaKey = "phone_number",
                        MetaValue = user.PhoneNumber
                    }
                };

                await UserMetaDataRepository.CreateAsync(metaInfoList);
            }


            return convertedUser.Id; 
        }

    }
}
