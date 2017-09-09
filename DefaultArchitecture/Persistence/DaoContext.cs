using DefaultArchitecture.Persistence.Map;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Persistence
{
    public class DaoContext : DbContext
    {
        public DaoContext()
            : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"Server=localhost;Database=architecture;Uid=root;Pwd=4tl4nt45;"); 
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
