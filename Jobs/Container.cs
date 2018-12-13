using Microsoft.Extensions.DependencyInjection;

namespace Jobs
{
    public static class Container
    {
        public static IServiceCollection AddJobs(this IServiceCollection services)
        {
            services.AddSingleton<Scheduler>();
            return services;
        }
    }
}
