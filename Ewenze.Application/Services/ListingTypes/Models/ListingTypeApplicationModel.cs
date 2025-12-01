namespace Ewenze.Application.Services.ListingType.Models
{
    public class ListingTypeApplicationModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Label { get; set; }
        public string? Icon { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
