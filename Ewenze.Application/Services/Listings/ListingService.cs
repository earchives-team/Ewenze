using Ewenze.Application.Exceptions;
using Ewenze.Application.Services.Listings.Exceptions;
using Ewenze.Application.Services.Listings.Models;
using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using System.Reflection;

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
                throw new NotFoundException(nameof(id), id);
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
               throw new NotFoundException("listingId", listing.Id);
            }
            listing.UpdatedAt = DateTime.UtcNow;
            var convertedListingV2 = ListingConverter.Convert(listing);
            await ListingRepository.UpdateAsync(convertedListingV2);
        }

        public async Task DeleteAsync(int id)
        {
            var existingListing = await ListingRepository.GetByIdAsync(id);
            if (existingListing == null)
            {
                throw new NotFoundException(nameof(id), id);
            }
            await ListingRepository.DeleteAsync(id);
        }

        public async Task UpdateListingStatusAsync(int id, string status)
        {
            var existingListing = await ListingRepository.GetByIdAsync(id);
            if (existingListing == null)
            {
                throw new NotFoundException(nameof(id), id);
            }
            var statusToUpdate = (ListingStatus)Enum.Parse(typeof(ListingStatus), status, true);

            // On ne p
            if (ListingStatus.ARCHIVE == existingListing.Status)
            {
                throw new BadRequestException(
                     $"The status {status} is not allowed to be set directly.",
                     "status",
                     $"The status {status} cannot be set directly."
                );
            }

            existingListing.Status = statusToUpdate;
            existingListing.UpdatedAt = DateTime.UtcNow;

            await ListingRepository.UpdateAsync(existingListing);
        }

        public async Task ArchiveAsync(int id)
        {
            var existingListing = await ListingRepository.GetByIdAsync(id);
            if (existingListing == null)
            {
                throw new NotFoundException("listingId", id);
            }

            if (ListingStatus.ARCHIVE == existingListing.Status)
            {
                return;
            }

            existingListing.Status = ListingStatus.ARCHIVE;
            existingListing.UpdatedAt = DateTime.UtcNow;
            await ListingRepository.UpdateAsync(existingListing);
        }
    }
}
