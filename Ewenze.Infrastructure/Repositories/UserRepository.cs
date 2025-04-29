using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using Ewenze.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EWenzeDbContext _eWenzeDbContext;

        public UserRepository(EWenzeDbContext eWenzeDbContext)
        {
            this._eWenzeDbContext = eWenzeDbContext;
        }

        public async Task<User> CreateUser(User user)
        {
            await _eWenzeDbContext.AddAsync(user);
            await _eWenzeDbContext.SaveChangesAsync();
            return user;
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _eWenzeDbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _eWenzeDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _eWenzeDbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByUsernameOrEmail(string value)
        {
            return await _eWenzeDbContext.Users
                .FirstOrDefaultAsync(x =>
                                    x.LoginName.Equals(value, StringComparison.OrdinalIgnoreCase) ||
                                    x.Email.Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task CreateUserMetadataAsync(IEnumerable<UserMeta> userMetas)
        {
            await _eWenzeDbContext.UserMetas.AddRangeAsync(userMetas);
            await _eWenzeDbContext.SaveChangesAsync();
        }

        public async Task<Dictionary<string, string?>> GetUserMetaDictionnaryAsync(int userId, List<string> metaKeys)
        {
            var metas = await _eWenzeDbContext.UserMetas
                         .Where(m => m.UserId == userId && metaKeys.Contains(m.MetaKey))
                         .ToListAsync();

            return metas.ToDictionary(m => m.MetaKey, m => m.MetaValue);
        }
    }
}
