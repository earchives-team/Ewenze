using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Ewenze.API.Models.UsersDto
{
    public class CreateUserInputDto
    {
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserOutputDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? phoneNumber { get; set; }
        public string Email { get; set; }
    }
}
