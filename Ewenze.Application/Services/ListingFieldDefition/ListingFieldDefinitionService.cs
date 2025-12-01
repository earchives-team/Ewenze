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
                throw new ListingFieldDefinitionException("Listing field definition does not exist")
                {
                    Reason = ListingFieldDefinitionExceptionReason.EntityNotFound,
                    InvalidProperty = "listingFieldDefinitionId"
                };
            }

            return ListingFieldDefinitionConverter.Convert(listingFieldDefinition);
        }
    }
}
