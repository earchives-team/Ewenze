using Ewenze.Application.Authentication;
using Ewenze.Application.Models.AuthModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ewenze.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authentificationService;

        public AuthController(IAuthService authentificationService)
        {
            _authentificationService = authentificationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            if(request == null)
            {
                return BadRequest(new
                {
                    email = "This field is required.",
                    password = "this field is correct."
                });
            }

            if (string.IsNullOrWhiteSpace(request.LoginInformation))
            {
                return BadRequest(new
                {
                    loginInformation = "This field is required"
                }); 
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    password = "This field is required"
                });
            }

            return Ok(await _authentificationService.Login(request));
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPasswordRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authentificationService.ForgotPassword(forgotPasswordRequest.Email);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authentificationService.ResetPassword(resetPasswordRequest.Email, resetPasswordRequest.NewPassword, resetPasswordRequest.Token);
            return Ok();
        }
    }
}
