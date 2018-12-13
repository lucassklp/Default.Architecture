using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Default.Architecture
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://0.0.0.0:5000")
                .UseStartup<Startup>()
                .Build();
    }
}
