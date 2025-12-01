using Ewenze.Application.Services.ListingFieldDefition.Models;

namespace Ewenze.Application.Services.ListingFieldDefition
{
    public interface IListingFieldDefinitionService
    {
        Task<IEnumerable<ListingFieldDefinitionApplicationModel>> GetAllAsync();

        Task<ListingFieldDefinitionApplicationModel> GetByIdAsync(int id);
    }
}
