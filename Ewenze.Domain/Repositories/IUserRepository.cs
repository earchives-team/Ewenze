using Ewenze.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserV2>> GetUsersAsync(); 
        Task<UserV2?> GetUserByEmailAsync(string email);
        Task<UserV2?> GetUserByIdAsync(int id);
        Task<UserV2> CreateUserAsync(UserV2 user); 
        Task UpdateUserAsync(UserV2 user);
        Task DeleteUserAsync(int id);
    }
}
