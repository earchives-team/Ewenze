using System.ComponentModel.DataAnnotations;

namespace Ewenze.Application.Models.AuthModel
{
    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
