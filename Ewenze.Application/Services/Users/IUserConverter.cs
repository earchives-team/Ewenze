using Ewenze.Application.Services.Users.Models;

namespace Ewenze.Application.Services.Users
{
    public interface IUserConverter
    {
        IList<UserApplicationModel> Convert(IEnumerable<Domain.Entities.UserV2> users);
        UserApplicationModel Convert(Domain.Entities.UserV2 user); 
    }
}
