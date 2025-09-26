using Ewenze.Domain.Entities;

namespace Ewenze.Domain.Repositories
{
    public interface IListingTypeRepository
    {
        Task<IEnumerable<Entities.ListingType>> GetListingTypesAsync();
        Task<ListingType?> GetListingTypeById(int id);
    }
}
