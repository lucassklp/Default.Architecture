using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    public abstract class AbstractRepository<TPrimaryKey, TEntity> : Crud<TPrimaryKey, TEntity>
        where TPrimaryKey : IComparable, IConvertible, IComparable<TPrimaryKey>, IEquatable<TPrimaryKey>
        where TEntity : class, Identifiable<TPrimaryKey>
    {
        public AbstractRepository(DbContext context) : base(context)
        {
        }

        protected DbSet<TEntity> Set()
        {
            return this.Context.Set<TEntity>();
        }
    }


    public abstract class AbstractRepository<TEntity> : Crud<long, TEntity>
        where TEntity : class, Identifiable<long>
    {
        public AbstractRepository(DbContext context) : base(context)
        {
        }

        protected DbSet<TEntity> Set()
        {
            return this.Context.Set<TEntity>();
        }
    }
}
