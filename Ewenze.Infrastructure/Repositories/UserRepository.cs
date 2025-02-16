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

        public async Task CreateUser(User user)
        {
            await _eWenzeDbContext.AddAsync(user);
            await _eWenzeDbContext.SaveChangesAsync();
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

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
