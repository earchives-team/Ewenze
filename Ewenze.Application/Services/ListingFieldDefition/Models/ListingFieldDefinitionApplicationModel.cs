using System.Text.Json.Nodes;

namespace Ewenze.Application.Services.ListingFieldDefition.Models
{
    public class ListingFieldDefinitionApplicationModel
    {
        public int Id { get; set; }

        public int ListingTypeId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        // JSONB
        public JsonObject Schema { get; set; } = new();

        public int Version { get; set; } = 1;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
