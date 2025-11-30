using Ewenze.Application.Services.Listings.Exceptions;
using Ewenze.Application.Services.Listings.Models;
using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;

namespace Ewenze.Application.Services.Listings
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository ListingRepository;
        private readonly IListingConverter ListingConverter;

        public ListingService(IListingRepository listingRepository, IListingConverter listingConverter)
        {
            ListingRepository = listingRepository ?? throw new ArgumentNullException(nameof(listingRepository));
            ListingConverter = listingConverter ?? throw new ArgumentNullException(nameof(listingConverter));
        }

        public async Task<IEnumerable<Listing>> GetAllAsync()
        {
            var listingData = await ListingRepository.GetAsync();

            return ListingConverter.Convert(listingData);
        }

        public async Task<Listing> GetByIdAsync(int id)
        {
            var listingData = await ListingRepository.GetByIdAsync(id);

            if (listingData == null)
            {
                throw new ListingException($"The Listing with id {id} was not found")
                {
                    Reason = ListingExceptionReason.EntityNotFound,
                    InvalidProperty = "listingId"
                };
            }

            return ListingConverter.Convert(listingData);
        }

        public async Task<int> CreateAsync(Listing listing)
        {
            var convertedListingV2 = ListingConverter.Convert(listing);
            var listingData = await ListingRepository.CreateAsync(convertedListingV2);

            return listingData.Id;
        }
    }
}
