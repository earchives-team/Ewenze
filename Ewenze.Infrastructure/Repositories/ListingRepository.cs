using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using Ewenze.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Ewenze.Infrastructure.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly EWenzeDbContext EWenzeDbContext;

        public ListingRepository(EWenzeDbContext eWenzeDbContext)
        {
            this.EWenzeDbContext = eWenzeDbContext;
        }

        public async Task<IEnumerable<ListingV2>> GetAsync()
        {
            return await EWenzeDbContext.ListingV2s.ToListAsync();
        }

        public async Task<ListingV2?> GetByIdAsync(int id)
        {
            return await EWenzeDbContext.ListingV2s.FindAsync(id);
        }

        public async Task<ListingV2> CreateAsync(ListingV2 listing)
        {
           await EWenzeDbContext.AddAsync(listing);
           await  EWenzeDbContext.SaveChangesAsync();
           return listing;
        }

        public async Task UpdateAsync(ListingV2 listing)
        {
            EWenzeDbContext.ListingV2s.Update(listing);
            await EWenzeDbContext.SaveChangesAsync();
        }
    }
}
