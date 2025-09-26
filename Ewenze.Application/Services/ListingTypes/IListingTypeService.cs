namespace Ewenze.Application.Services.ListingTypes
{
    public interface IListingTypeService
    {
        Task<IEnumerable<ListingType.Models.ListingType>> GetAllListingTypesAsync();
        Task<ListingType.Models.ListingType> GetListingTypeByIdAsync(int id);
    }
}
