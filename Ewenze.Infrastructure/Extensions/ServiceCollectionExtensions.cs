
using Ewenze.Application.Authentication;
using Ewenze.Application.Models;
using Ewenze.Domain.Repositories;
using Ewenze.Infrastructure.DatabaseContext;
using Ewenze.Infrastructure.Repositories;
using Ewenze.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ewenze.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEwenzeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<EWenzeDbContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("EWenze")!);
            });


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthService, AuthService>();

            return services;
        }
    }
}
