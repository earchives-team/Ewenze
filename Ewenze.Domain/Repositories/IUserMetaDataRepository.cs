using Ewenze.Domain.Entities;

namespace Ewenze.Domain.Repositories
{
    public interface IUserMetaDataRepository
    {
        Task<IEnumerable<UserMeta>> GetByMetaKeysAsync(List<string> metakeys);
        Task<IEnumerable<UserMeta>> GetByUserIdBAndByMetaKeysAsync(int userId, List<string> metaKeys);
        Task CreateUserMetadataAsync(IEnumerable<UserMeta> users);
    }
}
