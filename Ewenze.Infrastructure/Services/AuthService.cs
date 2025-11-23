using BCrypt.Net;
using Ewenze.Application.Authentication;
using Ewenze.Application.EMailManagement;
using Ewenze.Application.Models;
using Ewenze.Application.Models.AuthModel;
using Ewenze.Application.Services.Users.Exceptions;
using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IEmailService _emailService;

        public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings, IEmailService emailService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        #region Login 
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            
            if(request.LoginInformation == null)
                throw new UnauthorizedAccessException("Email or password are incorrect");
            
            // The user can Login with the email or username 
            var user = await _userRepository.GetUserByUsernameOrEmail(request.LoginInformation);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Email or password are incorrect");
            }

            JwtSecurityToken jwtSecurityToken = GenerateToken(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.LoginName
            };

            return response;
        }
        #endregion

        #region Forgot Password

        public async Task ForgotPassword(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            var user = await _userRepository.GetUserByUsernameOrEmail(email);

            if (user == null)
                throw new UsersException("email not valid")
                {
                    Reason = UsersExceptionReason.EntityNotFound,
                    InvalidProperty = "email"
                };


            /*
             Au lieu de generer un Token 
            Utiliser un OTP (One Time Password) Le mettre en Db 

             
             */
            var otp = GenerateOTP();
            var hashedOtp = HashOTP(otp);

            var token = GeneratePasswordResetToken(user);

            //if(token != null)
            //{
            //    // L'url est temporaire est devrait etre mis dans le appSettings.json
            //    var resetLink = $"https://yourapp.com/reset-password?token={new JwtSecurityTokenHandler().WriteToken(token)}";
            //    await _emailService.SendPasswordResetEmailAsync(user.Email, resetLink);
            //}

        }
        #endregion

        #region Reset Password
        public async Task ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var user = await _userRepository.GetUserByEmailAsync(resetPasswordRequest.Email);

            if (string.IsNullOrWhiteSpace(resetPasswordRequest.Email) || string.IsNullOrWhiteSpace(resetPasswordRequest.NewPassword) || string.IsNullOrWhiteSpace(resetPasswordRequest.Otp))
                throw new ArgumentException("Invalid parameters");

            if (user == null)
                throw new UsersException("email not valid")
                {
                    Reason = UsersExceptionReason.EntityNotFound,
                    InvalidProperty = "email"
                };

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken;

            try
            {
                jwtToken = handler.ReadJwtToken(token);
            }
            catch
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            // 1️ Vérifier l’expiration
            if (jwtToken.ValidTo < DateTime.UtcNow)
                throw new SecurityTokenExpiredException("Token has expired");

            // 2️ Vérifier le type de token
            var tokenType = jwtToken.Claims.FirstOrDefault(c => c.Type == "token_type")?.Value;
            if (tokenType != "reset_password")
                throw new SecurityTokenException("Invalid token type");

            // 3️ Vérifier l’identité de l’utilisateur
            var tokenEmail = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
            if (!string.Equals(tokenEmail, email, StringComparison.OrdinalIgnoreCase))
                throw new SecurityTokenException("Token does not match user email");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.Password = hashedPassword;

            await _userRepository.UpdateUser(user);
        }
        #endregion

        #region Logout
        public Task Logout()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Generate Token
        private JwtSecurityToken GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.LoginName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("token_type", "access")
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));


            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );


            return jwtSecurityToken;

        }
        #endregion

        #region Generate Password Reset Token
        private JwtSecurityToken GeneratePasswordResetToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.LoginName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("token_type", "reset_password")
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15), // Password reset token valid for 30 minutes
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            return jwtSecurityToken;
        }
        #endregion

        #region Generate OPT 
        private string GenerateOTP()
        {
            var random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }
        private string HashOTP(string otp)
        {
            return BCrypt.Net.BCrypt.HashPassword(otp);
        }

        #endregion
    }
}
