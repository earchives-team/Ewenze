using Ewenze.Application.Services.ListingFieldDefition.Models;

namespace Ewenze.Application.Services.ListingFieldDefition
{
    public interface IListingFieldDefinitionConverter
    {
        IEnumerable<ListingFieldDefinitionApplicationModel> Convert(IEnumerable<Domain.Entities.ListingFieldDefinition> listingFieldDefinitions);
        ListingFieldDefinitionApplicationModel Convert(Domain.Entities.ListingFieldDefinition listingFieldDefinition);
        
    }
}
