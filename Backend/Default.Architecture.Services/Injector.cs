using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;
using Default.Architecture.Services.Validation;

namespace Default.Architecture.Services
{
    public static class Injector
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddRepository();
            services.AddValidators();
            services.AddTransient<LoginServices>();
            services.AddTransient<UserServices>();

            return services;
        }
    }
}
