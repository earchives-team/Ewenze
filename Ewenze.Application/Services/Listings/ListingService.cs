using Ewenze.Application.Services.Listings.Exceptions;
using Ewenze.Application.Services.Listings.Models;
using Ewenze.Domain.Repositories;

namespace Ewenze.Application.Services.Listings
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IListingConverter _listingConverter;

        public ListingService(IListingRepository listingRepository, IListingConverter listingConverter)
        {
            _listingRepository = listingRepository ?? throw new ArgumentNullException(nameof(listingRepository));
            _listingConverter = listingConverter ?? throw new ArgumentNullException(nameof(listingConverter));
        }

        public async Task<IEnumerable<Listing>> GetAllAsync()
        {
            var listingData = await _listingRepository.GetAsync();

            return _listingConverter.Convert(listingData);
        }

        public async Task<Listing> GetByIdAsync(int id)
        {
            var listingData = await _listingRepository.GetById(id);

            if (listingData == null)
            {
                throw new ListingException($"The Listing with id {id} was not found")
                {
                    Reason = ListingExceptionReason.EntityNotFound,
                    InvalidProperty = "listingId"
                };
            }

            return _listingConverter.Convert(listingData);
        }
    }
}
