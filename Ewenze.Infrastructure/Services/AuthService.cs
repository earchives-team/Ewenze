using BCrypt.Net;
using Ewenze.Application.Authentication;
using Ewenze.Application.EMailManagement;
using Ewenze.Application.Exceptions;
using Ewenze.Application.Models;
using Ewenze.Application.Models.AuthModel;
using Ewenze.Domain.Entities;
using Ewenze.Domain.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IUserRepository UserRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IEmailService _emailService;

        public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings, IEmailService emailService)
        {
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        #region Login 
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            if (request.Email == null)
                throw new UnauthorizedAccessException("Email or password are incorrect");

            var user = await UserRepository.GetUserByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Email or password are incorrect");
            }

            JwtSecurityToken jwtSecurityToken = GenerateToken(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.Name
            };

            return response;
        }
        #endregion

        #region Forgot Password

        public async Task ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            var user = await UserRepository.GetUserByEmailAsync(email);

            if (user == null)
                throw new BadRequestException("Email is not valid");

            var otp = GenerateOTP();
            var hashedOtp = HashOTP(otp);

            user.Otp = hashedOtp;
            user.OtpExpiration = DateTime.UtcNow.AddMinutes(15);

            await UserRepository.UpdateUserAsync(user);
            await _emailService.SendPasswordResetEmailAsync(user.Email, otp);
        }
        #endregion

        #region Reset Password
        public async Task ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            if (string.IsNullOrWhiteSpace(resetPasswordRequest.Email) || string.IsNullOrWhiteSpace(resetPasswordRequest.NewPassword) || string.IsNullOrWhiteSpace(resetPasswordRequest.Otp))
                throw new BadRequestException("Invalid parameters");

            var user = await UserRepository.GetUserByEmailAsync(resetPasswordRequest.Email);


            if (user == null)
                throw new Application.Exceptions.NotFoundException(nameof(user), resetPasswordRequest.Email);

            if (user.Otp == null)
                throw new BadRequestException("OTP is invalid");

            if (user.OtpExpiration < DateTime.UtcNow)
                throw new BadRequestException("OTP has expired");
              
            if (!BCrypt.Net.BCrypt.Verify(resetPasswordRequest.Otp, user.Otp))
                throw new BadRequestException("OTP is invalid");
              

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(resetPasswordRequest.NewPassword);
            user.PasswordHash = hashedPassword;

            await UserRepository.UpdateUserAsync(user);
        }
        #endregion

        #region Logout
        public Task Logout()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Generate Token
        private JwtSecurityToken GenerateToken(UserV2 user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
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
