using Ewenze.Domain.Entities;

namespace Ewenze.Domain.Repositories
{
    public interface IListingRepository
    {
        Task<IEnumerable<ListingV2>> GetAsync();
        Task<ListingV2?> GetByIdAsync(int id);
        Task<ListingV2> CreateAsync(ListingV2 listingType);
        Task UpdateAsync(ListingV2 listingType);
    }
}
