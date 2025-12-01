using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Ewenze.Domain.Entities
{
    public class ListingFieldDefinition
    {
        public int Id { get; set; }

        public int ListingTypeId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        // JSONB
        public JsonObject Schema { get; set; } = new();

        public int Version { get; set; } = 1;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        public ListingTypeV2? ListingType { get; set; }
    }

}
