using Ewenze.Application.Services.ListingFieldDefition.Models;
using Ewenze.Domain.Repositories;

namespace Ewenze.Application.Services.ListingFieldDefition
{
    public interface IListingFieldDefinitionService
    {
        Task<IEnumerable<ListingFieldDefinitionApplicationModel>> GetAllAsync();

        Task<ListingFieldDefinitionApplicationModel> GetByIdAsync(int id);
    }

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

        public Task<ListingFieldDefinitionApplicationModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IListingFieldDefinitionConverter
    {
        IEnumerable<ListingFieldDefinitionApplicationModel> Convert(IEnumerable<Domain.Entities.ListingFieldDefinition> listingFieldDefinitions);
        ListingFieldDefinitionApplicationModel Convert(Domain.Entities.ListingFieldDefinition listingFieldDefinition);
        
    }

    public class ListingFieldDefinitionConverter : IListingFieldDefinitionConverter
    {
        public IEnumerable<ListingFieldDefinitionApplicationModel> Convert(IEnumerable<Domain.Entities.ListingFieldDefinition> listingFieldDefinitions)
        {
            return listingFieldDefinitions.Select(l => Convert(l));
        }
        public ListingFieldDefinitionApplicationModel Convert(Domain.Entities.ListingFieldDefinition listingFieldDefinition)
        {
            return new ListingFieldDefinitionApplicationModel
            {
                Id = listingFieldDefinition.Id,
                Name = listingFieldDefinition.Name,
                Description = listingFieldDefinition.Description,
                ListingTypeId = listingFieldDefinition.ListingTypeId,
                Schema = listingFieldDefinition.Schema,
                Version = listingFieldDefinition.Version,
                CreatedAt = listingFieldDefinition.CreatedAt.UtcDateTime,
                UpdatedAt = listingFieldDefinition.UpdatedAt.UtcDateTime
            };
        }
    }
}
