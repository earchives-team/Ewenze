using Ewenze.Application.Services.Users.Models;

namespace Ewenze.Application.Services.Users
{
    public interface IUsersService
    {
        Task<IList<UserApplicationModel>> GetAllAsync(); 
        Task<UserApplicationModel> GetById(int userId);
        Task<UserApplicationModel> GetByEmailAsync(string email);
        Task<UserApplicationModel> CreateAsync(UserApplicationModel user); 
    }
}
