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
        Task<IEnumerable<User>> GetUsersAsync(); 
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUsernameOrEmail(string username);
        Task CreateUser(User user); 
        Task UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
