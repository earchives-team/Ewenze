using System.ComponentModel.DataAnnotations;

namespace Ewenze.Application.Models.AuthModel
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
