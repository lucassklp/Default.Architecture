using FluentScheduler;
using System;

namespace Jobs.Jobs
{
    class TestJob : IJob
    {
        public void Execute()
        {
            DateTime dt = DateTime.Now;
            Console.WriteLine($"Job was executed at {dt.ToLongTimeString()}");
        }
    }
}
