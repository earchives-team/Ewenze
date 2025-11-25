using System.ComponentModel.DataAnnotations;

namespace Ewenze.API.Models.UsersDto
{
    public class UserInputDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
