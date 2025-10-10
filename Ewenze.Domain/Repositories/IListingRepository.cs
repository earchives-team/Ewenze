using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Domain.Repositories
{
    public interface IListingRepository
    {
        Task<IEnumerable<Entities.Listing>> GetAsync();
        Task<Entities.Listing?> GetById(int id);
    }
}
