namespace Ewenze.Domain.Entities
{
    public class ListingTypeV2
    {
        public int Id { get; set; }

        public string Label { get; set; } = default!;
        public string? Description { get; set; }
        public string? Icon { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    }
}
