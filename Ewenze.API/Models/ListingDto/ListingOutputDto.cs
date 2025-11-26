namespace Ewenze.API.Models.ListingDto
{
    public class ListingOutputDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string CategoryPath { get; set; } = default!;
        public decimal? Price { get; set; }
        public string PriceCurrency { get; set; } = "EUR";
        public string? City { get; set; }
        public string? CoverImage { get; set; }
        public List<string>? Images { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
