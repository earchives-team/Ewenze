
using Ewenze.Application.Exceptions;
using Ewenze.Domain.Repositories;

namespace Ewenze.Application.Services.ListingTypes
{
    public class ListingTypeService : IListingTypeService
    {
        private readonly IListingTypeRepository ListingTypeRepository;
        private readonly IListingTypeConverter ListingConverter;

        public ListingTypeService(IListingTypeRepository listingTypeRepository, IListingTypeConverter listingConverter)
        {
            this.ListingTypeRepository = listingTypeRepository;
            this.ListingConverter = listingConverter;
        }

        public async Task<IEnumerable<ListingType.Models.ListingTypeApplicationModel>> GetAllListingTypesAsync()
        {
            var listingData = await ListingTypeRepository.GetListingTypesAsync();

            return ListingConverter.Convert(listingData);
        }

        public async Task<ListingType.Models.ListingTypeApplicationModel> GetListingTypeByIdAsync(int id)
        {
            var listingData = await ListingTypeRepository.GetListingTypeById(id);
            if (listingData == null)
            {
                throw new NotFoundException(nameof(id), id);
            }

            return ListingConverter.Convert(listingData);
        }
    }
}
