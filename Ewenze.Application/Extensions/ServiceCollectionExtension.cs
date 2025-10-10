using Ewenze.Application.Services.Listings;
using Ewenze.Application.Services.ListingTypes;
using Ewenze.Application.Services.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;


namespace Ewenze.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            services.AddAutoMapper(applicationAssembly);

            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

            services.AddTransient<IUsersService, UsersService>();
            services.AddSingleton<IUserConverter, UserConverter>();

            services.AddTransient<IListingTypeService, ListingTypeService>();
            services.AddSingleton<IListingTypeConverter, ListingTypeConverter>();

            services.AddTransient<IListingService, ListingService>();
            services.AddSingleton<IListingConverter, ListingConverter>();

            return services;
        }
    }
}
