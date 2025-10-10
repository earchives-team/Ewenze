using Ewenze.API.Models.ListingTypeDto;
using Ewenze.Application.Services.ListingType.Models;

namespace Ewenze.API.Converters
{
    public class ListingTypeConverter
    {
        public IList<ListingTypeOuputDto> Convert(IEnumerable<ListingType> listingTypes) => listingTypes?.Select(Convert).ToList();

        public ListingTypeOuputDto Convert(ListingType listingType)
        {
            if (listingType == null) return null;

            return new ListingTypeOuputDto
            {
                Id = listingType.Id,
                CreationDate = listingType.CreationDate,
                ModifiedDate = listingType.ModifiedDate,
                Status = listingType.Status,
                Title = listingType.Title
            };
        }

        public ListingType Convert(ListingTypeInputDto listingTypeInputDto)
        {
            // Should be convert from ListingTypeInputDto to ListingType
            // For now, inputDto is not supported in the UI, so returning a new instance
            return new ListingType();
        }
    }
}
