using Business;
using Business.Interfaces;
using Default.Architecture.Senders.Email;
using Default.Architecture.Senders.Email.Interfaces;
using Default.Architecture.Services;
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
            //Injecting the service for TemplateEmailSender
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<ITemplateEmailSender, TemplateEmailSender>();
            services.AddScoped(typeof(ICrud<,>), typeof(Crud<,>));

            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ValidatorService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserRepositoryAsync, UserRepository>();
        }
    }
}
