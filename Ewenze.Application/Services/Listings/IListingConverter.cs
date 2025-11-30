using Ewenze.Application.Services.Listings.Models;
using Ewenze.Domain.Entities;

namespace Ewenze.Application.Services.Listings
{
    public interface IListingConverter
    {
        IEnumerable<ListingApplicationModel> Convert(IEnumerable<Domain.Entities.ListingV2> listings);
        ListingApplicationModel Convert(Domain.Entities.ListingV2 listing);
        ListingV2 Convert(Models.ListingApplicationModel listing);
    }
}
