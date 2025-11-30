using Ewenze.Application.Services.Listings.Models;
using Ewenze.Domain.Entities;
using System.Text.Json.Nodes;

namespace Ewenze.Application.Services.Listings
{
    public class ListingConverter : IListingConverter
    {
        public IEnumerable<Models.ListingApplicationModel> Convert(IEnumerable<Domain.Entities.ListingV2> listings)
        {
            return listings.Select(l => Convert(l));
        }

        public Models.ListingApplicationModel Convert(ListingV2 listing)
        {
            return new Models.ListingApplicationModel
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
        public ListingV2 Convert(Models.ListingApplicationModel listing)
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
                DynamicFields = ConvertToJsonObject(listing.Fields), // TODO: Convertir les champs dynamiques en JsonObject
                Status = listing.Status,
                IsFeatured = listing.IsFeatured,
                ViewCount = listing.ViewCount,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }

        private JsonObject? ConvertToJsonObject(Dictionary<string, object>? dict)
        {
            if (dict == null)
                return null;

            var jsonObject = new JsonObject();

            foreach (var kvp in dict)
            {
                jsonObject[kvp.Key] = ConvertToJsonNode(kvp.Value);
            }

            return jsonObject;
        }

        private JsonNode? ConvertToJsonNode(object? value)
        {
            return value switch
            {
                null => null,
                string s => JsonValue.Create(s),
                int i => JsonValue.Create(i),
                long l => JsonValue.Create(l),
                double d => JsonValue.Create(d),
                float f => JsonValue.Create(f),
                decimal dec => JsonValue.Create(dec),
                bool b => JsonValue.Create(b),
                DateTime dt => JsonValue.Create(dt),
                DateTimeOffset dto => JsonValue.Create(dto),

                // Pour les listes/arrays
                IEnumerable<object> list => new JsonArray(list.Select(ConvertToJsonNode).ToArray()),

                // Pour les dictionnaires imbriqués
                Dictionary<string, object> nestedDict => ConvertToJsonObject(nestedDict),

                // Fallback: essayer de sérialiser en string
                _ => JsonValue.Create(value.ToString())
            };
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
                               v.TryGetValue(out long l) ? l :
                               v.TryGetValue(out double d) ? d :
                               v.TryGetValue(out bool b) ? b :
                               v.TryGetValue(out DateTime dt) ? dt :
                               v.ToString(),
                JsonArray arr => arr.Select(ConvertJsonNode).ToList(),
                JsonObject o => ConvertDynamicFields(o),
                _ => node.ToString()
            };
        }
    }
}
