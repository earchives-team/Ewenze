using Ewenze.Application.Services.Users.Models;

namespace Ewenze.Application.Services.Users
{
    public class UserConverter : IUserConverter
    {
        public IList<User> Convert(IEnumerable<(Domain.Entities.User user, string? firstName, string? lastName, string? phoneNumber)> users)
        {
            return users
                    .Select(u => 
                        Convert(u.user, u.firstName, u.lastName, u.phoneNumber))
                    .ToList();
        }

        public User Convert(Domain.Entities.User user, string? firstName, string? lastName, string? phoneNumber)
        {
            if(user == null) 
                return null;

            return new User
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                NiceName = user.NiceName,
                UserName = user.NiceName
            };
        }
    }
}
