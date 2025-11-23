using Ewenze.Application.Models.AuthModel;

namespace Ewenze.Application.Authentication
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task Logout();
        Task ForgotPassword(string email);
        Task ResetPassword(ResetPasswordRequest resetPasswordRequest);
    }
}
