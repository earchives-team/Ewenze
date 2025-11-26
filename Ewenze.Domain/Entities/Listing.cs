using System.Text.Json.Nodes;

namespace Ewenze.Domain.Entities
{
    public class ListingV2
    {
        public int Id { get; set; }

        // Foreign Keys
        public int ListingTypeId { get; set; }
        public int UserId { get; set; }

        public string CategoryPath { get; set; } = default!;

        // Informations de base
        public string Title { get; set; } = default!;
        public string? Description { get; set; }

        // Prix
        public decimal? Price { get; set; }
        public string PriceCurrency { get; set; } = "EUR";

        // Localisation
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string Country { get; set; } = "France";

        // Dates
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }

        // Médias
        public List<string>? Images { get; set; }          // JSONB
        public string? CoverImage { get; set; }

        // Métadonnées
        public List<string>? Tags { get; set; }          // TEXT[]
        public int ViewCount { get; set; } = 0;
        public bool IsFeatured { get; set; } = false;

        // Champs dynamiques
        public JsonObject? DynamicFields { get; set; }   // JSONB // Should find best way to store dynamic fields and send data

        // Statut
        public ListingStatus Status { get; set; } = ListingStatus.DRAFT;

        // Timestamps
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        // Navigation properties
        public ListingTypeV2? ListingType { get; set; }
        public UserV2? User { get; set; }
    }
}
