using Ewenze.Application.Services.Listings.Models;

namespace Ewenze.Application.Services.Listings
{
    public interface IListingConverter
    {
        IEnumerable<Listing> Convert(IEnumerable<Domain.Entities.Listing> listings);
        Listing Convert(Domain.Entities.Listing listing);
    }
}
