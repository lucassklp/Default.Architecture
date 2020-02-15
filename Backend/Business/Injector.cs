using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;
using Business.Validation;

namespace Business
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
