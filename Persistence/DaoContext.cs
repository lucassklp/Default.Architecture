using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Map;
using System;

namespace Persistence
{
    public class DaoContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        public DaoContext(DbContextOptions<DaoContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            this._loggerFactory = loggerFactory;
            var a = this._loggerFactory.CreateLogger("Teste");
            a.LogDebug("Log feito com sucesso");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(_loggerFactory);

        }


        /// <summary>
        /// Manipulate a Table using Entity Framework
        /// </summary>
        /// <typeparam name="T">Entity to Manipulate</typeparam>
        /// <returns>A DbSet Object to manipulate the database.</returns>
        public DbSet<T> Manipulate<T>() where T : class
        {
            return base.Set<T>();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());

            base.OnModelCreating(modelBuilder);
        }


    }
}
