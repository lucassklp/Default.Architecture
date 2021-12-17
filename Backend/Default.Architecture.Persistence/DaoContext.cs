using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence.Extensions;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Reflection;

namespace Persistence
{
    public class DaoContext : DbContext
    {
        private ILoggerFactory _loggerFactory;
        private IConfiguration configuration;
        public DaoContext(DbContextOptions<DaoContext> options, ILoggerFactory loggerFactory, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
            this._loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
            optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"), serverVersion);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.UseSnakeCaseNamingConvention();
            base.OnModelCreating(modelBuilder);
        }
    }
}
