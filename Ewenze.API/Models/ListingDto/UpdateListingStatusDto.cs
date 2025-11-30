using System.ComponentModel.DataAnnotations;

namespace Ewenze.API.Models.ListingDto
{
    public class UpdateListingStatusDto
    {
        [Required]
        public string Status { get; set; } = default!;
    }
}
