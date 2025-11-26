using Ewenze.Application.Services.Listings.Models;

namespace Ewenze.Application.Services.Listings
{
    public interface IListingConverter
    {
        IEnumerable<Listing> Convert(IEnumerable<Domain.Entities.ListingV2> listings);
        Listing Convert(Domain.Entities.ListingV2 listing);
    }
}
