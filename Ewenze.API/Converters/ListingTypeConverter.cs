using Ewenze.API.Models.ListingTypeDto;
using Ewenze.Application.Services.ListingType.Models;

namespace Ewenze.API.Converters
{
    public class ListingTypeConverter
    {
        public IList<ListingTypeOuputDto> Convert(IEnumerable<ListingTypeApplicationModel> listingTypes) => listingTypes?.Select(Convert).ToList();

        public ListingTypeOuputDto Convert(ListingTypeApplicationModel listingType)
        {
            if (listingType == null) return null;

            return new ListingTypeOuputDto
            {
                Id = listingType.Id,
                CreationDate = listingType.CreationDate,
                ModifiedDate = listingType.ModifiedDate,
                Label = listingType.Label,
                Title = listingType.Title
            };
        }

        public ListingTypeApplicationModel Convert(ListingTypeInputDto listingTypeInputDto)
        {
            return new ListingTypeApplicationModel
            {
                Id = listingTypeInputDto.Id,
                CreationDate = listingTypeInputDto.CreationDate,
                ModifiedDate = listingTypeInputDto.ModifiedDate,
                Label = listingTypeInputDto.Label,
                Title = listingTypeInputDto.Title
            };
        }
    }
}
