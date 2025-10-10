namespace Ewenze.API.Models.ListingDto
{
    public class ListingOutputDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
