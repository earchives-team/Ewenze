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
            /*
             * Dans le context de wordPress les types de post sont stockés dans la table wp_posts
             * Pour ce qui est de Listing Type, le post_type est "case27_listing_type"
             */
            return await _eWenzeDbContext.PostTypes
                .Where(pt => pt.PostType == ListingTypeKey)
                .Select(pt => new ListingType
                {
                    Id = pt.Id,
                    Title = pt.PostTitle,
                    Status = pt.PostStatus,
                    CreationDate = pt.PostDate,
                    ModifiedDate = pt.PostModified
                })
                .ToListAsync();
        }

        public async Task<ListingType?> GetListingTypeById(int id)
        {
            return await _eWenzeDbContext.PostTypes
                .Where(pt => pt.PostType == ListingTypeKey && pt.Id == id)
                .Select(pt => new ListingType
                {
                    Id = pt.Id,
                    Title = pt.PostTitle,
                    Status = pt.PostStatus,
                    CreationDate = pt.PostDate,
                    ModifiedDate = pt.PostModified
                })
                .FirstOrDefaultAsync();
        }
    }
}
