using Ewenze.Application.Services.Listings.Models;

namespace Ewenze.Application.Services.Listings
{
    public class ListingConverter : IListingConverter
    {
        public IEnumerable<Listing> Convert(IEnumerable<Domain.Entities.Listing> listings)
        {
            return listings.Select(l => Convert(l));
        }
        public Listing Convert(Domain.Entities.Listing listing)
        {
            return new Listing
            {
                Id = listing.Id,
                Title = listing.Title,
                Description = listing.Description,
                Status = listing.Status,
                ModifiedDate = listing.ModifiedDate,
                CreationDate = listing.CreationDate
            };
        }
    }
}
