using System.ComponentModel.DataAnnotations;

namespace Ewenze.Application.Models.AuthModel
{
    public class AuthRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
