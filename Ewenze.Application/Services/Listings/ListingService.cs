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

        public async Task<IEnumerable<ListingApplicationModel>> GetAllAsync()
        {
            var listingData = await ListingRepository.GetAsync();

            return ListingConverter.Convert(listingData);
        }

        public async Task<ListingApplicationModel> GetByIdAsync(int id)
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

        public async Task<int> CreateAsync(ListingApplicationModel listing)
        {
            var convertedListingV2 = ListingConverter.Convert(listing);
            var listingData = await ListingRepository.CreateAsync(convertedListingV2);

            return listingData.Id;
        }

        public async Task UpdateAsync(ListingApplicationModel listing)
        {
            var existingListing = await ListingRepository.GetByIdAsync(listing.Id);
            if (existingListing == null)
            {
                throw new ListingException($"The Listing with id {listing.Id} was not found")
                {
                    Reason = ListingExceptionReason.EntityNotFound,
                    InvalidProperty = "listingId"
                };
            }
            var convertedListingV2 = ListingConverter.Convert(listing);
            await ListingRepository.UpdateAsync(convertedListingV2);
        }

        public async Task DeleteAsync(int id)
        {
            var existingListing = await ListingRepository.GetByIdAsync(id);
            if (existingListing == null)
            {
                throw new ListingException($"The Listing with id {id} was not found")
                {
                    Reason = ListingExceptionReason.EntityNotFound,
                    InvalidProperty = "listingId"
                };
            }
            await ListingRepository.DeleteAsync(id);
        }
    }
}
