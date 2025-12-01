using Ewenze.API.Models.ListingDto;
using Ewenze.Application.Services.Listings.Models;
using Ewenze.Domain.Entities;

namespace Ewenze.API.Converters
{
    public class ListingConverter
    {
        public IList<ListingOutputDto> Convert(IEnumerable<ListingApplicationModel> listings) => listings?.Select(Convert).ToList();

        public ListingOutputDto Convert(ListingApplicationModel listing)
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
                Images = listing.Images,
                DynamicFields = listing.Fields,
                CreatedAt = listing.CreatedAt
            };
        }

        public ListingApplicationModel Convert(ListingInputDto dto)
        {
            if (dto == null)
                return null;

            return new ListingApplicationModel
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

                Status = (ListingStatus) Enum.Parse(typeof(ListingStatus), dto.Status, true),
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,

                Tags = dto.Tags?.ToList() ?? new List<string>(),

                Fields = dto.Fields
            };
        }
    }
}
