using Microsoft.Extensions.DependencyInjection;

namespace Jobs
{
    public static class Injector
    {
        public static IServiceCollection AddJobs(this IServiceCollection services)
        {
            services.AddSingleton<Scheduler>();
            return services;
        }
    }
}
