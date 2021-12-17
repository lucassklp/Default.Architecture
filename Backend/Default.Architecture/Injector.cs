using Default.Architecture.Services;
using Default.Architecture.Authentication;
using Default.Architecture.Authentication.Jwt;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Default.Architecture
{
    public static class Injector
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddServices();

            services.AddScoped<IAuthenticator<ICredential>, JwtAuthenticator>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
