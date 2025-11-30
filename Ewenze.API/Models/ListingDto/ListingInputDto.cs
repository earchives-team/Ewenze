using Ewenze.Domain.Entities;

namespace Ewenze.API.Models.ListingDto
{
    public class  ListingInputDto
    {
        public int ListingTypeId { get; set; }
        public int UserId { get; set; }

        public string CategoryPath { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string? Description { get; set; }

        public decimal? Price { get; set; }
        public string PriceCurrency { get; set; } = "EUR";

        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public List<string>? Images { get; set; }
        public string? CoverImage { get; set; }

        public List<string>? Tags { get; set; }
        public Dictionary<string, object>? Fields { get; set; }
    }
}
