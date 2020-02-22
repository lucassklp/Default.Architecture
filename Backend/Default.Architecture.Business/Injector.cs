using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;
using Default.Architecture.Business.Validation;

namespace Default.Architecture.Business
{
    public static class Injector
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddRepository();
            services.AddValidators();
            services.AddTransient<LoginServices>();
            services.AddTransient<UserServices>();

            return services;
        }
    }
}
