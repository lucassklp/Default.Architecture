using FluentScheduler;
using Jobs.Jobs;

namespace Jobs
{
    public class Scheduler : Registry
    {
        public void Schedule()
        {
            Schedule<TestJob>().ToRunNow().AndEvery(5).Minutes();
        }
    }
}
