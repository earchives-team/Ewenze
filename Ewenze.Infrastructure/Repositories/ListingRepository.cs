using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using Ewenze.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Infrastructure.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly EWenzeDbContext _eWenzeDbContext;
        private const string ListingKey = "job_listing";

        public ListingRepository(EWenzeDbContext eWenzeDbContext)
        {
            this._eWenzeDbContext = eWenzeDbContext;
        }

        public async Task<IEnumerable<Listing>> GetAsync()
        {
            /*
             * Dans le context de wordPress Listing sont des posts de type "job_listing"
             * 
             */
            return await _eWenzeDbContext.PostTypes
                .Where(pt => pt.PostType == ListingKey)
                .Select(pt => new Listing
                {
                    Id = pt.Id,
                    Title = pt.PostTitle,
                    Description = pt.PostContent,
                    Status = pt.PostStatus,
                    CreationDate = pt.PostDate,
                    ModifiedDate = pt.PostModified
                })
                .ToListAsync();
        }

        public async Task<Listing?> GetById(int id)
        {
            return await _eWenzeDbContext.PostTypes
                .Where(pt => pt.PostType == ListingKey && pt.Id == id)
                .Select(pt => new Listing
                {
                    Id = pt.Id,
                    Title = pt.PostTitle,
                    Description = pt.PostContent,
                    Status = pt.PostStatus,
                    CreationDate = pt.PostDate,
                    ModifiedDate = pt.PostModified
                })
                .FirstOrDefaultAsync();
        }
    }
}
