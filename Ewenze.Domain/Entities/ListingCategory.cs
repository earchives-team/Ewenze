using System.Text.Json.Nodes;

namespace Ewenze.Domain.Entities
{
    public class ListingCategory
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

            // Navigation property (facultatif)
            public ListingTypeV2? ListingType { get; set; }
        }

    }
}
