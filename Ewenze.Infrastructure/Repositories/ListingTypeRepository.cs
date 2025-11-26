using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using Ewenze.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Ewenze.Infrastructure.Repositories
{
    public class ListingTypeRepository : IListingTypeRepository
    {
        private readonly EWenzeDbContext _eWenzeDbContext;
        private const string ListingTypeKey = "case27_listing_type";


        public ListingTypeRepository(EWenzeDbContext eWenzeDbContext)
        {
            this._eWenzeDbContext = eWenzeDbContext;
        }

        public async Task<IEnumerable<ListingType>> GetListingTypesAsync()
        {
           throw new NotImplementedException();
        }

        public async Task<ListingType?> GetListingTypeById(int id)
        {
           throw new NotImplementedException();
        }
    }
}
