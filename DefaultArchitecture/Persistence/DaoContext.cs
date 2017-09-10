using DefaultArchitecture.Persistence.Map;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DefaultArchitecture.Domain;
using System.IO;

namespace DefaultArchitecture.Persistence
{
    public class DaoContext : DbContext

    {
        public DaoContext(DbContextOptions<DaoContext> options)
            : base(options)
        {

        }

        public DaoContext()
            : base()
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"));
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
            base.OnModelCreating(modelBuilder);
        }


    }
}
