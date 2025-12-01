using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using Ewenze.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Ewenze.Infrastructure.Repositories
{
    public class ListingTypeRepository : IListingTypeRepository
    {
        private readonly EWenzeDbContext EWenzeDbContext;


        public ListingTypeRepository(EWenzeDbContext eWenzeDbContext)
        {
            this.EWenzeDbContext = eWenzeDbContext;
        }

        public async Task<IEnumerable<ListingTypeV2>> GetListingTypesAsync()
        {
           return await EWenzeDbContext.ListingTypeV2s.ToListAsync();
        }

        public async Task<ListingTypeV2?> GetListingTypeById(int id)
        {
          return await EWenzeDbContext.ListingTypeV2s.FindAsync(id);
        }
    }
}
