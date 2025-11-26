using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Domain.Entities
{
    public class ListingType
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreationDate { get; set; }
         
    }

    public class ListingTypeV2
    {
        public int Id { get; set; }

        public string Label { get; set; } = default!;
        public string? Description { get; set; }
        public string? Icon { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        public ICollection<ListingTypeV2>? Listings { get; set; }
    }
}
