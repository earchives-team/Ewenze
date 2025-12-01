using Ewenze.Application.Services.ListingFieldDefition.Models;

namespace Ewenze.Application.Services.ListingFieldDefition
{
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
