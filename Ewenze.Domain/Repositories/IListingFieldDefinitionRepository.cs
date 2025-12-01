namespace Ewenze.Domain.Repositories
{
    public interface IListingFieldDefinitionRepository
    {
        Task<List<Entities.ListingFieldDefinition>> GetAllAsync();
        Task<Entities.ListingFieldDefinition?> GetByIdAsync(int id);
        Task<List<Entities.ListingFieldDefinition>> GetByListingTypeAsync(int listingTypeId);
    }
}
