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
                Title = listing.Title,
                CategoryPath = listing.CategoryPath,
                Price = listing.Price,
                PriceCurrency = listing.PriceCurrency,
                City = listing.City,
                CoverImage = listing.CoverImage,
                CreatedAt = listing.CreatedAt
            };
        }

        public Listing Convert(ListingInputDto dto)
        {
            if (dto == null)
                return null;

            return new Listing
            {
                ListingTypeId = dto.ListingTypeId,
                UserId = dto.UserId,

                CategoryPath = dto.CategoryPath,
                Title = dto.Title,
                Description = dto.Description,

                Price = dto.Price,
                PriceCurrency = dto.PriceCurrency,

                City = dto.City,
                PostalCode = dto.PostalCode,
                Country = dto.Country ?? "France",

                Latitude = dto.Latitude,
                Longitude = dto.Longitude,

                Images = dto.Images?.ToList() ?? new List<string>(),
                CoverImage = dto.CoverImage,

                Tags = dto.Tags?.ToList() ?? new List<string>(),

                Fields = dto.Fields != null
                    ? new Dictionary<string, object>(dto.Fields)
                    : null
            };
        }
    }
}
