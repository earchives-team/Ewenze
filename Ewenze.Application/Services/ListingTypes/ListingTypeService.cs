
using Ewenze.Application.Services.ListingTypes.Exceptions;
using Ewenze.Domain.Repositories;

namespace Ewenze.Application.Services.ListingTypes
{
    public class ListingTypeService : IListingTypeService
    {
        private readonly IListingTypeRepository _listingTypeRepository;
        private readonly IListingTypeConverter _listingConverter;

        public ListingTypeService(IListingTypeRepository listingTypeRepository, IListingTypeConverter listingConverter)
        {
            _listingTypeRepository = listingTypeRepository;
            this._listingConverter = listingConverter;
        }

        public async Task<IEnumerable<ListingType.Models.ListingType>> GetAllListingTypesAsync()
        {
            var listingData = await _listingTypeRepository.GetListingTypesAsync();

            return _listingConverter.Convert(listingData);
        }

        public async Task<ListingType.Models.ListingType> GetListingTypeByIdAsync(int id)
        {
            var listingData = await _listingTypeRepository.GetListingTypeById(id);
            if (listingData == null)
            {
                throw new ListingTypeException($"The Listing Type with id {id} was not found")
                {
                    Reason = ListingTypeExceptionReason.EntityNotFound,
                    InvalidProperty = "userId"
                };
            }

            return _listingConverter.Convert(listingData);
        }
    }
}
