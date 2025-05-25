using Ewenze.Application.Services.Users.Models;
using Ewenze.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Services.Users
{
    public interface IUserConverter
    {
        IList<User> Convert(IEnumerable<(Domain.Entities.User user, string? firstName, string? lastName, string? phoneNumber)> users);
        User Convert(Domain.Entities.User user, string? firstName, string? lastName, string? phoneNumber); 
    }
}
