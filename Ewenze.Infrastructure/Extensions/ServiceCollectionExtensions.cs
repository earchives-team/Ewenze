
using Ewenze.Application.Authentication;
using Ewenze.Application.EMailManagement;
using Ewenze.Application.Models;
using Ewenze.Domain.Repositories;
using Ewenze.Infrastructure.DatabaseContext;
using Ewenze.Infrastructure.Persistence.Email;
using Ewenze.Infrastructure.Repositories;
using Ewenze.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ewenze.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEwenzeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddDbContext<EWenzeDbContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("EWenze")!);
            });


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserMetaDataRepository, UserMetaDataRepository>();
            services.AddTransient<IListingTypeRepository, ListingTypeRepository>();
            services.AddTransient<IListingRepository, ListingRepository>();
            services.AddTransient<IEmailService, EmailService>();


            // Mise en place de system d'authentification 

            services.AddAuthentication(options =>
            {
                // Je n'accept seulement le JWT comme system d'authentification pour api 
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SigningKey"]))
                };
            });



            return services;
        }
    }
}
