using Ewenze.Application.Services.Users.Models;

namespace Ewenze.Application.Services.Users
{
    public class UserConverter : IUserConverter
    {
        public IList<User> Convert(IEnumerable<Domain.Entities.UserV2> users)
        {
            return users.Select(u => Convert(u)).ToList();
        }

        public User Convert(Domain.Entities.UserV2 user)
        {
            if(user == null) 
                return null;

            return new User
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.Phone,
            };
        }
    }
}
