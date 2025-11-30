using Ewenze.Application.Services.Listings.Models;
using Ewenze.Domain.Entities;

namespace Ewenze.Application.Services.Listings
{
    public interface IListingConverter
    {
        IEnumerable<Listing> Convert(IEnumerable<Domain.Entities.ListingV2> listings);
        Listing Convert(Domain.Entities.ListingV2 listing);
        ListingV2 Convert(Models.Listing listing);
    }
}
