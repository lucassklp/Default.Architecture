using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace Business
{
    public static class Injector
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddRepository();

            services.AddTransient<LoginServices>();
            services.AddTransient<UserServices>();

            return services;
        }
    }
}
