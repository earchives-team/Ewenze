using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using Ewenze.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Ewenze.Infrastructure.Repositories
{
    public class UserMetaDataRepository : IUserMetaDataRepository
    {
        private readonly EWenzeDbContext _eWenzeDbContext;

        public UserMetaDataRepository(EWenzeDbContext eWenzeDbContext)
        {
            _eWenzeDbContext = eWenzeDbContext ?? throw new ArgumentNullException(nameof(eWenzeDbContext));
        }

        public async Task<IEnumerable<UserMeta>> GetByMetaKeysAsync(List<string> metakeys)
        {
            return await _eWenzeDbContext.UserMetas.Where(m => metakeys.Contains(m.MetaKey)).ToListAsync();
        }

        public async Task<IEnumerable<UserMeta>> GetByUserIdBAndByMetaKeysAsync(int userId, List<string> metaKeys)
        {
            var metas = await _eWenzeDbContext.UserMetas
                        .Where(m => m.UserId == userId && metaKeys.Contains(m.MetaKey))
                        .ToListAsync();

            return metas;
        }

        public async Task CreateUserMetadataAsync(IEnumerable<UserMeta> userMetas)
        {
            await _eWenzeDbContext.UserMetas.AddRangeAsync(userMetas);
            await _eWenzeDbContext.SaveChangesAsync();
        }
    }
}
