using Microsoft.Extensions.DependencyInjection;
using System;

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
