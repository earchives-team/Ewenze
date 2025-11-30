using Ewenze.Application.Services.Listings.Models;
using Ewenze.Domain.Entities;
using System.Text.Json.Nodes;

namespace Ewenze.Application.Services.Listings
{
    public class ListingConverter : IListingConverter
    {
        public IEnumerable<Models.Listing> Convert(IEnumerable<Domain.Entities.ListingV2> listings)
        {
            return listings.Select(l => Convert(l));
        }

        public Models.Listing Convert(ListingV2 listing)
        {
            return new Models.Listing
            {
                Id = listing.Id,

                ListingTypeId = listing.ListingTypeId,
                UserId = listing.UserId,

                CategoryPath = listing.CategoryPath,
                Title = listing.Title,
                Description = listing.Description,

                Price = listing.Price,
                PriceCurrency = listing.PriceCurrency,

                City = listing.City,
                PostalCode = listing.PostalCode,
                Country = listing.Country,

                Latitude = 0.0, //listing.LocationCoordinates?.Latitude,
                Longitude = 0.0,//listing.LocationCoordinates?.Longitude,

                Images = listing.Images,//ConvertImages(listing.Images),
                CoverImage = listing.CoverImage,

                Tags = listing.Tags?.ToList() ?? new List<string>(),

                StartDate = listing.StartDate?.UtcDateTime,
                EndDate = listing.EndDate?.UtcDateTime,

                Fields = ConvertDynamicFields(listing.DynamicFields),

                Status = listing.Status,
                IsFeatured = listing.IsFeatured,
                ViewCount = listing.ViewCount,

                CreatedAt = listing.CreatedAt.UtcDateTime,
                UpdatedAt = listing.UpdatedAt.UtcDateTime
            };
        }
        public ListingV2 Convert(Models.Listing listing)
        {
            return new ListingV2
            {
                Id = listing.Id,
                ListingTypeId = listing.ListingTypeId,
                UserId = listing.UserId,
                CategoryPath = listing.CategoryPath,
                Title = listing.Title,
                Description = listing.Description,
                Price = listing.Price,
                PriceCurrency = listing.PriceCurrency,
                City = listing.City,
                PostalCode = listing.PostalCode,
                Country = listing.Country,
                Images = listing.Images,
                CoverImage = listing.CoverImage,
                Tags = listing.Tags?.ToList() ?? new List<string>(),
                StartDate = listing.StartDate.HasValue ? DateTimeOffset.UtcNow : null,
                EndDate = listing.EndDate.HasValue ? DateTimeOffset.UtcNow : null,
                DynamicFields = null, // TODO: Convertir les champs dynamiques en JsonObject
                Status = listing.Status,
                IsFeatured = listing.IsFeatured,
                ViewCount = listing.ViewCount,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }

        private List<string> ConvertImages(JsonObject? imagesJson)
        {
            if (imagesJson == null)
                return new List<string>();

            // JSONB contenant une liste → ["img1","img2"]
            if (imagesJson.TryGetPropertyValue("items", out var listNode)
                && listNode is JsonArray arr)
            {
                return arr
                    .Select(x => x?.ToString() ?? "")
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList();
            }

            // Sinon, transformer toutes les valeurs en string
            return imagesJson
                .Select(kvp => kvp.Value?.ToString() ?? "")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();
        }

        private Dictionary<string, object>? ConvertDynamicFields(JsonObject? obj)
        {
            if (obj == null)
                return null;

            var dict = new Dictionary<string, object>();

            foreach (var kvp in obj)
            {
                dict[kvp.Key] = ConvertJsonNode(kvp.Value);
            }

            return dict;
        }

        private object ConvertJsonNode(JsonNode? node)
        {
            return node switch
            {
                null => null!,
                JsonValue v => v.TryGetValue(out int i) ? i :
                               v.TryGetValue(out double d) ? d :
                               v.TryGetValue(out bool b) ? b :
                               v.ToString(),

                JsonArray arr => arr.Select(ConvertJsonNode).ToList(),

                JsonObject o => ConvertDynamicFields(o),

                _ => node.ToString()
            };
        }
    }
}
