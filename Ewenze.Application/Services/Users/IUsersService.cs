using Ewenze.Application.Features.UserFeature.Commands.CreateUser;
using Ewenze.Application.Services.Users.Models;
using MediatR;

namespace Ewenze.Application.Services.Users
{
    public interface IUsersService
    {
        Task<IList<User>> GetAllAsync(); 
        Task<User> GetById(int userId);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUserNameAsync(string username);
        Task<int> Create(User user); 
    }
}
