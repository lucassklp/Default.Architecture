using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Persistence.Repository
{
    public static class Injector
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddPersistence();

            services.AddScoped(typeof(Crud<,>));
            services.AddScoped<TransactionScope>();

            services.AddTransient<UserRepository>();

            return services;
        }
    }
}
