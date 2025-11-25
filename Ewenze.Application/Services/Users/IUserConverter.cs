using Ewenze.Application.Services.Users.Models;

namespace Ewenze.Application.Services.Users
{
    public interface IUserConverter
    {
        IList<User> Convert(IEnumerable<Domain.Entities.UserV2> users);
        User Convert(Domain.Entities.UserV2 user); 
    }
}
