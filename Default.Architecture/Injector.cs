using Business;
using Business.Interfaces;
using Default.Architecture.Authentication;
using Default.Architecture.Authentication.Jwt;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Interfaces;

namespace Default.Architecture
{
    public static class Injector
    {
        /// <summary>
        /// This extension method inject the things you need into your application
        /// </summary>
        /// <param name="services"></param>
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICrud<,>), typeof(Crud<,>));
            services.AddScoped<IAuthenticator<ICredential>, JwtAuthenticator>();

            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ILoginServices, LoginServices>();
            services.AddTransient<ValidatorService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserRepositoryAsync, UserRepository>();
        }
    }
}
