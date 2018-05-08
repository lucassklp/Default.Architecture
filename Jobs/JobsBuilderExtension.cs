
using FluentScheduler;
using Jobs;

namespace Microsoft.AspNetCore.Builder
{
    public static class JobsBuilderExtension
    {
        public static IApplicationBuilder UseJobs(this IApplicationBuilder builder)
        {
            JobManager.Start();
            var service = builder.ApplicationServices.GetService(typeof(Scheduler)) as Scheduler;
            service.Schedule();
            JobManager.Initialize(service);
            return builder;
        }
    }
}
