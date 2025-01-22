
using Ewenze.Domain.Repositories;
using Ewenze.Infrastructure.DatabaseContext;
using Ewenze.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ewenze.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEwenzeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EWenzeDbContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("EWenze")!);
            });


            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
