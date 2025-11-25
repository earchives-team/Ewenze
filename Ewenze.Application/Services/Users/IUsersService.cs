using Ewenze.Application.Services.Users.Models;

namespace Ewenze.Application.Services.Users
{
    public interface IUsersService
    {
        Task<IList<User>> GetAllAsync(); 
        Task<User> GetById(int userId);
        Task<User> GetByEmailAsync(string email);
        Task<int> CreateAsync(User user); 
    }
}
