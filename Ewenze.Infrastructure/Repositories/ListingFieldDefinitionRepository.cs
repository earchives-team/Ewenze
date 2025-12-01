using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using Ewenze.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Ewenze.Infrastructure.Repositories
{
    public class ListingFieldDefinitionRepository : IListingFieldDefinitionRepository
    {
        private readonly EWenzeDbContext EWenzeDbContext;

        public ListingFieldDefinitionRepository(EWenzeDbContext eWenzeDbContext)
        {
            EWenzeDbContext = eWenzeDbContext;
        }

        public async Task<List<ListingFieldDefinition>> GetAllAsync()
        {
            return await EWenzeDbContext.ListingFieldDefinitions.ToListAsync();
        }

        public async Task<ListingFieldDefinition?> GetByIdAsync(int id)
        {
           return await EWenzeDbContext.ListingFieldDefinitions.FindAsync(id);
        }
    }
}
