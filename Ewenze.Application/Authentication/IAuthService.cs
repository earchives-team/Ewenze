using Ewenze.Application.Models.AuthModel;

namespace Ewenze.Application.Authentication
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
    }
}
