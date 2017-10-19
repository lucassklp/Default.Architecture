using Microsoft.EntityFrameworkCore;
using Persistence.Map;
using System;

namespace Persistence
{
    public class DaoContext : DbContext

    {
        public DaoContext(DbContextOptions<DaoContext> options)
            : base(options)
        {

        }

        //public DaoContext()
        //    : base()
        //{

        //}


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
