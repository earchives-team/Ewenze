using Ewenze.Application.Exceptions;
using Ewenze.Application.Services.ListingFieldDefition.Exceptions;
using Ewenze.Application.Services.ListingFieldDefition.Models;
using Ewenze.Domain.Repositories;

namespace Ewenze.Application.Services.ListingFieldDefition
{
    public class ListingFieldDefinitionService : IListingFieldDefinitionService
    {
        public readonly IListingFieldDefinitionRepository ListingFieldDefinitionRepository;
        public readonly IListingFieldDefinitionConverter ListingFieldDefinitionConverter;

        public ListingFieldDefinitionService(IListingFieldDefinitionRepository listingFieldDefinitionRepository, IListingFieldDefinitionConverter listingFieldDefinitionConverter)
        {
            ListingFieldDefinitionRepository = listingFieldDefinitionRepository ?? throw new ArgumentNullException(nameof(listingFieldDefinitionRepository));
            this.ListingFieldDefinitionConverter = listingFieldDefinitionConverter ?? throw new ArgumentNullException(nameof(listingFieldDefinitionConverter));
        }

        public async Task<IEnumerable<ListingFieldDefinitionApplicationModel>> GetAllAsync()
        {
            var listingFieldDefinitions = await ListingFieldDefinitionRepository.GetAllAsync();

            return ListingFieldDefinitionConverter.Convert(listingFieldDefinitions);
        }

        public async Task<ListingFieldDefinitionApplicationModel> GetByIdAsync(int id)
        {
            var listingFieldDefinition = await ListingFieldDefinitionRepository.GetByIdAsync(id);

            if (listingFieldDefinition == null)
            {
                throw new NotFoundException(nameof(id), id);
            }

            return ListingFieldDefinitionConverter.Convert(listingFieldDefinition);
        }

        public async Task<IEnumerable<ListingFieldDefinitionApplicationModel>> GetByListingTypeAsync(int listingTypeId)
        {
            var listingFieldDefinitions = await ListingFieldDefinitionRepository.GetByListingTypeAsync(listingTypeId);
            if (listingFieldDefinitions == null)
                throw new NotFoundException(nameof(listingTypeId), listingTypeId);

            return ListingFieldDefinitionConverter.Convert(listingFieldDefinitions);
        }
    }
}
