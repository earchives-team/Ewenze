namespace Ewenze.Application.Services.Listings
{
    public interface IListingService
    {
        Task<IEnumerable<Listings.Models.Listing>> GetAllAsync();
        Task<Listings.Models.Listing> GetByIdAsync(int id);
    }
}
