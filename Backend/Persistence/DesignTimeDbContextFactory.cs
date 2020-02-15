using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.IO;
using System.Linq;

namespace Persistence
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DaoContext>
    {
        public DaoContext CreateDbContext(string[] args)
        {
            var config = Directory.GetParent(Directory.GetCurrentDirectory())
                            .GetDirectories()
                            .Where(x => x.Name == "Default.Architecture")?.First()?.FullName;

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? EnvironmentName.Development;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(config)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env}.json")
                .Build();

            var builder = new DbContextOptions<DaoContext>();
            return new DaoContext(builder, NullLoggerFactory.Instance, configuration);
        }
    }
}
