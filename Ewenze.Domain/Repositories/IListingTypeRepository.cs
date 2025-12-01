using Ewenze.Domain.Entities;

namespace Ewenze.Domain.Repositories
{
    public interface IListingTypeRepository
    {
        Task<IEnumerable<Entities.ListingTypeV2>> GetListingTypesAsync();
        Task<ListingTypeV2?> GetListingTypeById(int id);
    }
}
