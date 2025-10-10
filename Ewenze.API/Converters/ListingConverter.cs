using Ewenze.API.Models.ListingDto;
using Ewenze.Application.Services.Listings.Models;

namespace Ewenze.API.Converters
{
    public class ListingConverter
    {
        public IList<ListingOutputDto> Convert(IEnumerable<Listing> listings) => listings?.Select(Convert).ToList();

        public ListingOutputDto Convert(Listing listing)
        {
            if (listing == null) return null;
            return new ListingOutputDto
            {
                Id = listing.Id,
                CreationDate = listing.CreationDate,
                ModifiedDate = listing.ModifiedDate,
                Status = listing.Status,
                Title = listing.Title,
                Description = listing.Description,
            };
        }

        public Listing Convert(ListingInputDto listingInputDto)
        {
            // Should be convert from ListingInputDto to Listing
            // For now, inputDto is not supported in the UI, so returning a new instance
            return new Listing();
        }
    }
}
