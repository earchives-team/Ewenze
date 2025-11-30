using Ewenze.Application.Services.Users.Models;

namespace Ewenze.Application.Services.Users
{
    public class UserConverter : IUserConverter
    {
        public IList<UserApplicationModel> Convert(IEnumerable<Domain.Entities.UserV2> users)
        {
            return users.Select(u => Convert(u)).ToList();
        }

        public UserApplicationModel Convert(Domain.Entities.UserV2 user)
        {
            if(user == null) 
                return null;

            return new UserApplicationModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.Phone,
            };
        }
    }
}
