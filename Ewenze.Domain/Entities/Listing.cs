using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Ewenze.Domain.Entities
{
    public  class Listing
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreationDate { get; set; }

    }

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
        public string? LocationCity { get; set; }
        public string? LocationPostalCode { get; set; }
        public string LocationCountry { get; set; } = "France";
        public Point? LocationCoordinates { get; set; } // POINT

        // Dates
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }

        // Médias
        public JsonObject? Images { get; set; }          // JSONB
        public string? CoverImage { get; set; }

        // Métadonnées
        public List<string>? Tags { get; set; }          // TEXT[]
        public int ViewCount { get; set; } = 0;
        public bool IsFeatured { get; set; } = false;

        // Champs dynamiques
        public JsonObject? DynamicFields { get; set; }   // JSONB

        // Statut
        public string Status { get; set; } = "DRAFT";

        // Timestamps
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        // Navigation properties
        public ListingTypeV2? ListingType { get; set; }
        public UserV2? User { get; set; }
    }
}
