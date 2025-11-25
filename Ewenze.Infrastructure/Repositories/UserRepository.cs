using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using Ewenze.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Ewenze.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EWenzeDbContext _eWenzeDbContext;

        public UserRepository(EWenzeDbContext eWenzeDbContext)
        {
            this._eWenzeDbContext = eWenzeDbContext;
        }

        public async Task<UserV2> CreateUserAsync(UserV2 user)
        {
            await _eWenzeDbContext.AddAsync(user);
            await _eWenzeDbContext.SaveChangesAsync();
            return user;
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserV2?> GetUserByEmailAsync(string email)
        {
            return await _eWenzeDbContext.UserV2s.FirstOrDefaultAsync(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<UserV2?> GetUserByIdAsync(int id)
        {
            return await _eWenzeDbContext.UserV2s.FindAsync(id);
        }

        public async Task<IEnumerable<UserV2>> GetUsersAsync()
        {
            return await _eWenzeDbContext.UserV2s.ToListAsync();
        }

        public async Task UpdateUserAsync(UserV2 user)
        {
            _eWenzeDbContext.UserV2s.Update(user);
            await _eWenzeDbContext.SaveChangesAsync();
        }
    }
}
