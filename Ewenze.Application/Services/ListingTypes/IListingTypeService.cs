namespace Ewenze.Application.Services.ListingTypes
{
    public interface IListingTypeService
    {
        Task<IEnumerable<ListingType.Models.ListingTypeApplicationModel>> GetAllListingTypesAsync();
        Task<ListingType.Models.ListingTypeApplicationModel> GetListingTypeByIdAsync(int id);
    }
}
