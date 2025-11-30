using Ewenze.Application.Services.Listings.Models;
using Ewenze.Domain.Entities;

namespace Ewenze.Application.Services.Listings
{
    public interface IListingService
    {
        Task<IEnumerable<Listings.Models.ListingApplicationModel>> GetAllAsync();
        Task<Listings.Models.ListingApplicationModel> GetByIdAsync(int id);
        Task<int> CreateAsync(ListingApplicationModel listing);
        Task UpdateAsync(ListingApplicationModel listing);
    }
}
