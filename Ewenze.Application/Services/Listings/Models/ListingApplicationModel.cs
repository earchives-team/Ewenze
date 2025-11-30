using Ewenze.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Services.Listings.Models
{
    public class ListingApplicationModel
    {
        public int Id { get; set; }

        public int ListingTypeId { get; set; }
        public int UserId { get; set; }

        public string CategoryPath { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public decimal? Price { get; set; }
        public string PriceCurrency { get; set; }

        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string Country { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public List<string> Images { get; set; }
        public string? CoverImage { get; set; }

        public List<string> Tags { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Dictionary<string, object>? Fields { get; set; }

        public ListingStatus Status { get; set; }

        public bool IsFeatured { get; set; }
        public int ViewCount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
